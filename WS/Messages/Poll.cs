using System;
using System.Collections.Generic;
// using System.Text.Json.Serialization;

namespace ChaosIV.WS.Messages
{
    public class Poll: IMessage
    {
        // [JsonPropertyName("type")]
        public string type { get; set; }

        // [JsonPropertyName("title")]
        public string title { get; set; }

        // [JsonPropertyName("choices")]
        //public string[] choices { get; set; }

        // [JsonPropertyName("duration")]
        public int duration { get; set; } = 60;

        // [JsonPropertyName("subscriberMultiplier")]
        public bool subscriberMultiplier { get; set; } = false;

        // [JsonPropertyName("subscriberOnly")]
        public bool subscriberOnly { get; set; } = false;
        
        // [JsonPropertyName("bits")]
        public int bits { get; set; } = 0;

        Dictionary<string, string> IMessage.data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}