using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GTA;
using GTA.Native;
using ChaosIV.WS.Messages;
// using System.Text.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Text.RegularExpressions;
using Fleck;

namespace ChaosIV.WS
{
	public class TwitchPoll
	{
		private readonly string _ffzPassphrase;
		private bool _isAuthenticated = false;
		private bool _waitPollResult = false;
		public Action OnCreated;
		public Action<PollResult> OnEnded;
		public Action OnAuth;

		private List<Fleck.IWebSocketConnection> connections = new List<Fleck.IWebSocketConnection>();

		public void StartServer()
		{
			/*
			try
			{
				WebSocketServer.Events.OnMessage += OnMessage;
				WebSocketServer.Events.OnClientDisconnected += OnClientDisconnect;
				WebSocketServer.Start("http://localhost:8081/");
				
				Game.Console.Print("Poll server started.");
				// Console.WriteLine("Press any key to exit...\n");
				// Console.ReadKey(true);
				// await WebSocketServer.StopAsync();
			}
			catch(OperationCanceledException)
			{
				// this is normal when tasks are canceled, ignore it
			}
			*/
			var server = new Fleck.WebSocketServer("ws://127.0.0.1:8081");
			server.Start(socket =>
			{
				socket.OnOpen = () => {
					connections.Add(socket);
				};
				socket.OnClose = () => {
					connections.Remove(socket);
					OnClientDisconnect();
				};
				socket.OnMessage += OnMessage;
			});
		}

		//private void OnMessage(ArraySegment<byte> buffer)
		private void OnMessage(string message)
		{
			//var message = Encoding.UTF8.GetString(buffer.Array, 0, buffer.Count);
			Game.Console.Print($"Received msg: {message}");
			try {
				// var poll = JsonSerializer.Deserialize<Poll>(message);
				var poll = JsonConvert.DeserializeObject<Poll>(message);
				
				switch (poll.type)
				{
					case "auth_ok":
						_isAuthenticated = true;
						OnAuth?.Invoke();
						break;

					// Client Passphrase
					case "auth":
						// Check passphrase
						// if (poll.Data === '123test') ...
						//Task.Run(() => SendAuthAsync());
						SendAuth();
					break;

					case "created":
						_waitPollResult = true;
						OnCreated?.Invoke();
					break;

					case "ended":
						_waitPollResult = false;
						// var pollResult = JsonSerializer.Deserialize<PollResult>(message);
						var pollResult = JsonConvert.DeserializeObject<PollResult>(message);
						OnEnded?.Invoke(pollResult);
					break;
				}
			}
			catch (JsonException ex)
			{
				ChaosMain.ReportException(ex);
			}
			// Regex expression = new Regex(@"""type"":""([^""]+)""");
			// var results = expression.Matches(message);

		}

		//   public void Send(IMessage message)
		//   {
		// var jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(message);
		//    Task.Run(() => WebSocketServer.BroadcastAsync(jsonUtf8Bytes));
		//   }
		private void Broadcast(string message)
		{
			connections.ForEach(connection => {
				if (!connection.IsAvailable)
					connection.Close();
				else
					connection.Send(message);
			});
		}

		public void Send(string message)
		{
			// Task.Run(() => SendAsync(message));
			Broadcast(message);
		}
		
		public void SendAuth()
		{
			Game.Console.Print("Send auth to FFZ.");

			Send($"{{\"type\":\"auth\",\"data\":\"{_ffzPassphrase}\"}}");
		}
		
		public async Task SendAsync(string message)
		{
			await WebSocketServer.BroadcastAsync(Encoding.UTF8.GetBytes(message));
		}
		
		public async Task SendAuthAsync()
		{
			Game.Console.Print("Send auth to FFZ.");

			await SendAsync($"{{\"type\":\"auth\",\"data\":\"{_ffzPassphrase}\"}}");
		}
				
		public async void CreatePollAsync(string[] choices, int durationSec) {	
			//Task taskAuth = null;
			if (!_isAuthenticated) {
				//taskAuth = SendAuthAsync();
				SendAuth();
			}
			if (_waitPollResult) {
				Game.Console.Print("Skipped CreatePoll: Still waiting for poll results...");
				//await taskAuth;
				return;
			}
			if (Game.Paused) {
				Game.Console.Print("Skipped CreatePoll: The game is paused");
				return;
			}

			Game.Console.Print("Trying to create a poll.");
			var choicesString = "\"" + string.Join("\", \"", choices) + "\"";
			var pollJson = @"{
				""type"": ""create"",
				""title"": ""[Chaos] Chose effect"",
				""choices"": [
				" + choicesString +	@"
				],
				""duration"": " + durationSec + @",
				""subscriberMultiplier"": false,
				""subscriberOnly"": false,
				""bits"": 0
			}";

			//if (taskAuth != null) {
			//	await taskAuth;
			//}
			//await SendAsync(pollJson);
			Send(pollJson);
		}

		public TwitchPoll(string ffzPassphrase)
		{
			this._ffzPassphrase = ffzPassphrase;
			
			Game.Console.Print("Waiting for Poll client connection...");

			StartServer();
		}

		private void OnClientDisconnect() 
		{
			_isAuthenticated = false;
			_waitPollResult = false;
		}

		public async void StopAsync()
		{
			await WebSocketServer.StopAsync();
		}
	}

}
