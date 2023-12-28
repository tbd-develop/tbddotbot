using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class StreamType
{
    public static readonly StreamType Live = new("live");
    public static readonly StreamType Playlist = new("playlist");
    public static readonly StreamType WatchParty = new("watch_party");
    public static readonly StreamType Premiere = new("premiere");
    public static readonly StreamType Rerun = new("rerun");
    public static readonly StreamType Unknown = new("unknown");

    private string Value { get; }

    private StreamType(string value)
    {
        Value = value;
    }

    public static implicit operator string(StreamType type) => type.Value;

    public static implicit operator StreamType(string type)
    {
        var availableTypes = typeof(StreamType)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(t => t.FieldType == typeof(StreamType))
            .Select(t => t.GetValue(null) as StreamType)
            .SingleOrDefault(t => t?.Value == type);

        return availableTypes ?? Unknown;
    }

    public class Converter : JsonConverter<StreamType>
    {
        public override StreamType Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            reader.GetString() ?? Unknown;

        public override void Write(
            Utf8JsonWriter writer,
            StreamType type,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(type);
    }
}