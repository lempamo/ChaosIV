using System;
using System.Diagnostics;

using ChaosIV.WS.Messages;

using GTA;

using Newtonsoft.Json;

namespace ChaosIV
{
	class PollProxy {
		private PipeServer _pipeServer;
		private Process _proxyProcess;

		private readonly string _ffzPassphrase;

		private bool _isAuthenticated = false;
		private bool _waitPollResult = false;

		public Action OnConnect;
		public Action OnDisconnect;
		public Action<Error> OnError;
		public Action OnCreate;
		public Action<PollResult> OnEnd;
		public Action OnAuth;

		public PollProxy(int port, string ffzPassphrase, string scriptPath) {
			_ffzPassphrase = ffzPassphrase;

			OnDisconnect += OnClientDisconnect;

			_pipeServer = new PipeServer();
			_pipeServer.OnMessage += OnMessage;

			_proxyProcess = new Process() {
				EnableRaisingEvents = false,
				StartInfo = {
					FileName = scriptPath + @"\for Developers\ChaosIVTwitchPollProxy\bin\Release\net45\ChaosIVTwitchPollProxy.exe",
					Arguments = $"{port}",
					RedirectStandardOutput = false,
					RedirectStandardInput = false,
					UseShellExecute = false,
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Hidden,
				},
			};
			_proxyProcess.Exited += new EventHandler((object s, EventArgs e) => {
				Game.Console.Print("ChaosIVTwitchPollProxy.exe closed.");
				//Stop();
			});

			var started = _proxyProcess.Start();
		}

		private void OnMessage(string message) {
			//Game.Console.Print($"Received msg: {message}");

			if (message == "connected") {
				Game.Console.Print("FFZ connected");
				OnConnect?.Invoke();
				return;
			}
			else if (message == "disconnected") {
				Game.Console.Print("FFZ disconnected");
				OnDisconnect?.Invoke();
				return;
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

					case "update":
						if (pollResult.poll.ended) {
							_waitPollResult = false;
							OnEnd?.Invoke(pollResult);
						}
						break;

					case "error":
						Game.Console.Print($"FFZ error: {message}");
						var error = JsonConvert.DeserializeObject<Error>(message);
						OnError?.Invoke(error);
						break;
				}
			}
			catch (JsonException ex) {
				ChaosMain.ReportException(ex);
			}
		}

		public void Send(string message) {
			_pipeServer.Send(message);
		}

		public void SendAuth() {
			if (_isAuthenticated) {
				return;
			}

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
				//SendAuth();
				return;
			}

			// Game.Console.Print("Trying to create a poll.");
			var poll = new Poll() {
				choices = choices,
				duration = durationSec,
			};

			Send(JsonConvert.SerializeObject(poll));
		}

		private void OnClientDisconnect() {
			_isAuthenticated = false;
			_waitPollResult = false;
		}

		public void Stop() {
			_pipeServer.Close();
			if (!_proxyProcess.HasExited) {
				_proxyProcess.Kill();
			}
		}
	}
}
