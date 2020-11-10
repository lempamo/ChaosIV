using System;
// using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ChaosIV.WS.Messages
{
    public interface IMessage
    {
        // [JsonPropertyName("type")]
        string type { get; set; }

        // [JsonPropertyName("data")]
        Dictionary<string, string> data { get; set; }
    }
}