namespace ChaosIV.WS.Messages
{
	public class Auth : IMessage
	{
		public string type { get; set; } = "auth";

		public string data { get; set; }
	}
}