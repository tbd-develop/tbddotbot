using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Infrastructure;

public class IncomingMessageType
{
    public static readonly IncomingMessageType Verification = new("webhook_callback_verification");
    public static readonly IncomingMessageType Notification = new("notification");
    public static readonly IncomingMessageType Revocation = new("revocation");
    public static readonly IncomingMessageType Unknown = new("unknown");

    private string Type { get; }

    private IncomingMessageType(string type)
    {
        Type = type;
    }

    public static implicit operator string(IncomingMessageType type) => type.Type;

    public static implicit operator IncomingMessageType(string type)
    {
        var availableTypes = typeof(IncomingMessageType)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(t => t.FieldType == typeof(IncomingMessageType))
            .Select(t => t.GetValue(null) as IncomingMessageType)
            .SingleOrDefault(t => t?.Type == type);

        return availableTypes ?? Unknown;
    }
    
    public class Converter : JsonConverter<IncomingMessageType>
    {
        public override IncomingMessageType Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            reader.GetString() ?? Unknown;

        public override void Write(
            Utf8JsonWriter writer,
            IncomingMessageType type,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(type);
    }
}