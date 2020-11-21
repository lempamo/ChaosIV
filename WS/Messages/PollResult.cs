using System.Collections.Generic;

namespace ChaosIV.WS.Messages
{
	public class PollResult: IMessage
	{
		public string type { get; set; }
		public UpdatedPoll poll { get; set; }
	}

	public class UpdatedPoll
	{
		public bool ended { get; set; }
		public IList<ChoiceResult> choices { get; set; }
	}

	public class ChoiceResult
	{
		public string text { get; set; }
		public int votes { get; set; }
	}
}
