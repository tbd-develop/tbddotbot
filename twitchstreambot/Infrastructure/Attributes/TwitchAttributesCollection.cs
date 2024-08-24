using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace twitchstreambot.Infrastructure.Attributes;

public class TwitchAttributesCollection : IEnumerable<TwitchAttribute>
{
    private readonly IEnumerable<TwitchAttribute> _attributes;

    public TwitchAttributesCollection(IEnumerable<TwitchAttribute> attributes)
    {
        _attributes = attributes;
    }

    public IEnumerator<TwitchAttribute> GetEnumerator()
    {
        return _attributes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _attributes.GetEnumerator();
    }

    public static implicit operator TwitchAttributesCollection(Dictionary<string, string> dictionary)
    {
        return new TwitchAttributesCollection(dictionary.Select(d => new TwitchAttribute()
            { Element = d.Key, Arguments = d.Value }));
    }
}