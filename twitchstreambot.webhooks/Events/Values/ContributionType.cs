using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace twitchstreambot.webhooks.Events.Values;

public class ContributionType
{
    public static ContributionType Bits = new("bits");
    public static ContributionType Subscription = new("subscription");
    public static ContributionType Other = new("other");

    private string Name { get; set; }

    private ContributionType(string name)
    {
        Name = name;
    }

    public static implicit operator string(ContributionType contributionType)
    {
        return contributionType.Name;
    }

    public static implicit operator ContributionType(string contributionType)
    {
        var availableTypes = typeof(ContributionType)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(t => t.FieldType == typeof(ContributionType))
            .Select(t => t.GetValue(null) as ContributionType)
            .SingleOrDefault(t => t?.Name == contributionType);

        return availableTypes ?? Other;
    }

    public class Converter : JsonConverter<ContributionType>
    {
        public override ContributionType Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            reader.GetString() ?? Other;

        public override void Write(
            Utf8JsonWriter writer,
            ContributionType type,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(type);
    }
}