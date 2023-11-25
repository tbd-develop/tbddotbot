﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using twitchbot.commands;
using twitchstreambot;
using twitchstreambot.api.Configuration;
using twitchstreambot.basics;
using twitchstreambot.command;
using twitchstreambot.command.CommandDispatch;
using twitchstreambot.Dispatch;
using twitchstreambot.Infrastructure;
using twitchstreambot.Infrastructure.Configuration;
using twitchstreambot.Infrastructure.Extensions;
using twitchstreambot.Parsing;
using twitchstreambot.pubsub;

namespace twitchbot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostedService(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostedService(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json")
                        .AddUserSecrets<BotService>();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddTwitchStreamBot(typeof(UptimeCommand).Assembly);

                    services.AddHelix(context.Configuration);
                    services.AddKraken(context.Configuration);
                    services.AddTwitchAPI(context.Configuration);

                    services.AddSingleton<TwitchPubSub>();

                    services.AddHostedService<BotService>();
                });
        }
    }
}