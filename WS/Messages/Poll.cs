namespace ChaosIV.WS.Messages
{
	public class Poll : IMessage
	{
		public string type { get; set; }
		public string title { get; set; }
		//public string[] choices { get; set; }
		public int duration { get; set; } = 60;
		public bool subscriberMultiplier { get; set; } = false;
		public bool subscriberOnly { get; set; } = false;
		public int bits { get; set; } = 0;
	}
}