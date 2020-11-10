using System;
using System.Collections.Generic;
// using System.Text.Json.Serialization;

namespace ChaosIV.WS.Messages
{
    public class PollResult: IMessage
    {
        // [JsonPropertyName("type")]
        public string type { get; set; }

        public IList<ChoiceResult> choices { get; set; }
        public Dictionary<string, string> data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class ChoiceResult
    {
        public string text { get; set; }
        public int votes { get; set; }
    }
}