using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace twitchstreambot.Infrastructure.Attributes
{
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
            throw new NotImplementedException();
        }

        public string this[string name]
        {
            get { return _attributes.SingleOrDefault(atr => atr.Element.Equals(name, StringComparison.InvariantCultureIgnoreCase))?.Arguments; }
        }

        public static implicit operator TwitchAttributesCollection(Dictionary<string, string> dictionary)
        {
            return new TwitchAttributesCollection(dictionary.Select(d => new TwitchAttribute()
            { Element = d.Key, Arguments = d.Value }));
        }
    }
}