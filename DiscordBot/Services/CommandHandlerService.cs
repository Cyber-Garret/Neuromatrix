﻿using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Services
{
	public class CommandHandlerService
	{
		#region Private fields
		private readonly DiscordShardedClient _client = Program.Client;
		private CommandService _commands;
		private readonly IServiceProvider _services;
		#endregion

		public CommandHandlerService(CommandService commands, IServiceProvider services)
		{
			_commands = commands;
			_services = services;
		}

		public async Task ConfigureAsync()
		{
			_commands = new CommandService(new CommandServiceConfig
			{
				DefaultRunMode = RunMode.Async,
				CaseSensitiveCommands = false
			});

			await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
		}

		public async Task HandleCommandAsync(SocketMessage arg)
		{
			// Ignore if not SocketUserMessage 
			if (!(arg is SocketUserMessage msg)) return;
			// Ignore if command execute in Private message
			//if (msg.Channel is SocketDMChannel) return;

			var context = new ShardedCommandContext(_client, msg);
			// Ignore all bots
			if (context.User.IsBot) return;

			string prefix;
			if (!context.IsPrivate)
			{
				var config = await FailsafeDbOperations.GetGuildAccountAsync(context.Guild.Id);
				prefix = config.CommandPrefix ?? "!";
			}
			else
				prefix = "!";
			

			var argPos = 0;
			// Ignore if not mention this bot or command not start from char !
			if (!(msg.HasMentionPrefix(_client.CurrentUser, ref argPos) || msg.HasStringPrefix(prefix, ref argPos))) return;
			{

				var cmdSearchResult = _commands.Search(context, argPos);
				if (cmdSearchResult.Commands == null)
				{
					await Logger.Log(new LogMessage(LogSeverity.Warning, "HandleCommand", $"Command {msg.Content} return {cmdSearchResult.Error}"));
					return;
				}

				var executionTask = _commands.ExecuteAsync(context, argPos, _services);

				await executionTask.ContinueWith(task =>
				 {
					 if (task.Result.IsSuccess || task.Result.Error == CommandError.UnknownCommand) return;
					 const string errTemplate = "{0}, {1}.";
					 var errMessage = string.Format(errTemplate, context.User.Mention, task.Result.ErrorReason);
					 context.Channel.SendMessageAsync(errMessage);
				 });
			}
		}

	}
}
