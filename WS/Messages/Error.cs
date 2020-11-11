namespace ChaosIV.WS.Messages
{
	public class Error : IMessage
	{
		public string type { get; set; } = "error";
		public string id { get; set; }
	}
}