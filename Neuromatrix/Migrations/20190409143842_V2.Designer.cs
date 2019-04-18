﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DiscordBot.Data;

namespace DiscordBot.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    [Migration("20190409143842_V2")]
    partial class V2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("Neuromatrix.Models.Db.Gear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Catalyst");

                    b.Property<string>("CatalystBonus");

                    b.Property<string>("CatalystQuest");

                    b.Property<string>("Description");

                    b.Property<string>("DropLocation");

                    b.Property<string>("IconUrl");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<string>("PerkDescription");

                    b.Property<string>("PerkName");

                    b.Property<string>("SecondPerkDescription");

                    b.Property<string>("SecondPerkName");

                    b.Property<string>("Type");

                    b.Property<string>("WhereCatalystDrop");

                    b.HasKey("Id");

                    b.ToTable("Gears");
                });

            modelBuilder.Entity("Neuromatrix.Models.Db.Guild", b =>
                {
                    b.Property<ulong>("ID");

                    b.Property<bool>("EnableLogging")
                        .HasDefaultValue(false);

                    b.Property<bool>("EnableNotification")
                        .HasDefaultValue(false);

                    b.Property<ulong>("LoggingChannel")
                        .HasDefaultValue(0ul);

                    b.Property<string>("Name");

                    b.Property<ulong>("NotificationChannel")
                        .HasDefaultValue(0ul);

                    b.Property<ulong>("OwnerId");

                    b.Property<string>("WelcomeMessage");

                    b.HasKey("ID");

                    b.ToTable("Guilds");
                });
#pragma warning restore 612, 618
        }
    }
}
