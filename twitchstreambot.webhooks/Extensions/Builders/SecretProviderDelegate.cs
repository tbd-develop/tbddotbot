using Microsoft.AspNetCore.Http;
using twitchstreambot.webhooks.Infrastructure;
using twitchstreambot.webhooks.Infrastructure.Contracts;

namespace twitchstreambot.webhooks.Extensions.Builders;

public delegate string SecretProviderDelegate(
    ISecretProvider provider,
    TwitchHeaderCollection headers,
    HttpRequest request);

public delegate string GetSecretDelegate(
    TwitchHeaderCollection headers,
    HttpRequest request);