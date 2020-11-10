using System;
// using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ChaosIV.WS.Messages
{
    public class Error: IMessage
    {
        // [JsonPropertyName("type")]
        public string type { get; set; } = "error";

        // [JsonPropertyName("data")]
        public Dictionary<string, string> data { get; set; }
    }
}