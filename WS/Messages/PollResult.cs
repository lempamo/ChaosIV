using System.Collections.Generic;

namespace ChaosIV.WS.Messages
{
    public class PollResult: IMessage
    {
        public string type { get; set; }
        public IList<ChoiceResult> choices { get; set; }
		//public string data { get; set; }
    }

    public class ChoiceResult
    {
        public string text { get; set; }
        public int votes { get; set; }
    }
}