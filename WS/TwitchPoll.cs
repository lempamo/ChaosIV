using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using ChaosIV.WS.Messages;

using GTA;
using GTA.Native;
// using System.Text.Json;
using Newtonsoft.Json;

namespace ChaosIV.WS
{
	public class TwitchPoll
	{
		private readonly int _port;
		private readonly string _ffzPassphrase;
		private bool _isAuthenticated = false;
		private bool _waitPollResult = false;
		public Action OnConnect;
		public Action OnCreate;
		public Action<PollResult> OnEnd;
		public Action OnAuth;

		private List<Fleck.IWebSocketConnection> connections = new List<Fleck.IWebSocketConnection>();

		public void StartServer() {
			var server = new Fleck.WebSocketServer($"ws://127.0.0.1:{_port}");
			server.RestartAfterListenError = false;

			server.Start(socket => {
				socket.OnOpen = () => {
					connections.Add(socket);
					OnConnect?.Invoke();
					Game.Console.Print("FFZ Client connected.");
				};
				socket.OnClose = () => {
					connections.Remove(socket);
					OnClientDisconnect();
					Game.Console.Print("FFZ Client disconnected.");
				};
				socket.OnMessage += OnMessage;
			});
		}

		private void OnMessage(string message) {
			//Game.Console.Print($"Received msg: {message}");

			//System.Threading.Thread.Sleep(3000);
			var millisecondsToWait = 3000;
			Stopwatch stopwatch = Stopwatch.StartNew();
			while (true) {
				if (stopwatch.ElapsedMilliseconds >= millisecondsToWait) {
					break;
				}
				System.Threading.Thread.Sleep(10);
			}

			try {
				var pollResult = JsonConvert.DeserializeObject<PollResult>(message);

				switch (pollResult.type) {
					// Client Passphrase
					case "auth":
						// Check passphrase
						// if (poll.data === '123test') ...
						SendAuth();
						break;

					case "auth_ok":
						_isAuthenticated = true;
						OnAuth?.Invoke();
						break;

					case "created":
						_waitPollResult = true;
						OnCreate?.Invoke();
						break;

					case "ended":
						_waitPollResult = false;
						OnEnd?.Invoke(pollResult);
						break;

					case "error":
						Game.Console.Print($"FFZ error: {message}");
						break;
				}
			}
			catch (JsonException ex) {
				ChaosMain.ReportException(ex);
			}
		}

		private void Broadcast(string message) {
			connections.ForEach(connection => {
				if (connection.IsAvailable) { 
					connection.Send(message);
				}
				else {
					connection.Close();
				}
			});
		}

		public void Send(string message) {
			Broadcast(message);
		}

		public void SendAuth() {
			Game.Console.Print("Send auth request to FFZ.");

			var authMessage = new Auth { 
				data = _ffzPassphrase
			};
			Send(JsonConvert.SerializeObject(authMessage));
		}

		public void CreatePoll(in string[] choices, in int durationSec) {
			if (_waitPollResult) {
				Game.Console.Print("Skipped CreatePoll: Still waiting for poll results...");
				return;
			}
			if (Game.Paused) {
				//Game.Console.Print("Skipped CreatePoll: The game is paused");
				System.Threading.Thread.Sleep(5000);
				CreatePoll(in choices, in durationSec);

				return;
			}
			if (!_isAuthenticated) {
				SendAuth();
			}

			// Game.Console.Print("Trying to create a poll.");
			var choicesString = "\"" + string.Join("\", \"", choices) + "\"";
			var pollJson = @"{
				""type"": ""create"",
				""title"": ""[Chaos] Chose effect"",
				""choices"": [
				" + choicesString + @"
				],
				""duration"": " + durationSec + @",
				""subscriberMultiplier"": false,
				""subscriberOnly"": false,
				""bits"": 0
			}";

			Send(pollJson);
		}

		public TwitchPoll(int port, string ffzPassphrase) {
			this._port = port;
			this._ffzPassphrase = ffzPassphrase;

			Game.Console.Print("Waiting for Poll client connection...");

			StartServer();
		}

		private void OnClientDisconnect() {
			_isAuthenticated = false;
			_waitPollResult = false;
		}
	}
}
