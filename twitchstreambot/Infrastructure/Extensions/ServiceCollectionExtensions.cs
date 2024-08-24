using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using twitchstreambot.Api;
using twitchstreambot.Dispatch;
using twitchstreambot.Infrastructure.Attributes;
using twitchstreambot.Infrastructure.Configuration;
using twitchstreambot.Infrastructure.Delegates;

namespace twitchstreambot.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTwitch(this IServiceCollection services,
        Action<TwitchConfigurationBuilder> configure)
    {
        var builder = new TwitchConfigurationBuilder(services);

        configure(builder);

        return services;
    }
}