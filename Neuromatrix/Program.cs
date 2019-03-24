﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Nett;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Neuromatrix.Models;
using Neuromatrix.Services;
using Neuromatrix.Modules.Administration;

namespace Neuromatrix
{
    class Program
    {
        #region Private fields
        public static DiscordShardedClient _client;
        private IServiceProvider _services;
        private static string _config_path;
        private readonly int[] _shardIds = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        #endregion


        private static void Main()
        {
            Console.Title = $"Neuromatrix (Discord.Net v{DiscordConfig.Version})";
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Blue;
            _config_path = Directory.GetCurrentDirectory() + "/UserData/config.tml";

            new Program().StartAsync().GetAwaiter().GetResult();
        }

        private async Task StartAsync()
        {
            _client = new DiscordShardedClient(_shardIds, new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                DefaultRetryMode = RetryMode.AlwaysRetry,
                MessageCacheSize = 100,
                TotalShards = 10
            });

            _client.Log += Logger.Log;

            #region Configure services
            _services = BuildServices();

            _services.GetRequiredService<ConfigurationService>().Configure();
            _services.GetRequiredService<ReminderService>().Configure();
            _services.GetRequiredService<DiscordEventHandlerService>().Configure();
            await _services.GetRequiredService<CommandHandlingService>().ConfigureAsync();
            #endregion

            var token = _services.GetRequiredService<Configuration>().Token;
            

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            await _client.SetGameAsync("Destiny 2", null, ActivityType.Watching);
            await _client.SetStatusAsync(UserStatus.Online);

            await Task.Delay(-1);
        }

        public ServiceProvider BuildServices()
        {
            return new ServiceCollection()
                .AddSingleton(_ => Toml.ReadFile<Configuration>(_config_path))
                .AddSingleton<ConfigurationService>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<RateLimitService>()
                .AddSingleton<DiscordEventHandlerService>()
                .AddSingleton<ReminderService>()
                .AddScoped<ServerActivityLogger>()
                .BuildServiceProvider();
        }

    }
}
