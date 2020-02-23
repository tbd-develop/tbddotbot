﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace twitchstreambot.api.Models
{
    public class StreamVideo
    {
        [JsonProperty("video_id")]
        public string VideoId { get; set; }
        public IEnumerable<StreamMarker> Markers { get; set; }
    }
}