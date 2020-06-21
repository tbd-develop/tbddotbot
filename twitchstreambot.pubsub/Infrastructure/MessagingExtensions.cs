using System;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace twitchstreambot.pubsub.Infrastructure
{
    public static class MessagingExtensions
    {
        public static ArraySegment<byte> AsJsonBytesArraySegment(this object @object, JsonSerializerSettings settings = null)
        {
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@object,
                settings ?? new JsonSerializerSettings
                { ContractResolver = new CamelCasePropertyNamesContractResolver() })));
        }

        public static byte[] AsJsonBytesArray(this object @object, JsonSerializerSettings settings = null)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@object,
                settings ?? new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }
    }
}