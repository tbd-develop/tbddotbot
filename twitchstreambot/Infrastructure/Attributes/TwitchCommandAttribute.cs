using System;

namespace twitchstreambot.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TwitchCommandAttribute(string actionName) : Attribute
{
    public string Action { get; } = actionName;
    public bool Ignore { get; set; }
    public bool IsPrivate { get; set; }
}