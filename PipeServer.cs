using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace ChaosIV
{
	class PipeServer
	{
		private readonly NamedPipeServerStream _pipe;
		private StreamReader _pipeReader;
		private StreamWriter _pipeWriter;

		private readonly GTA.Timer _pipeTimer = new GTA.Timer();
		public static readonly int PIPE_INTERVAL = 200;

		private Task<string> _readPipeTask;

		public Action<string> OnMessage;

		public PipeServer() {
			try {
				_pipe = new NamedPipeServerStream("ChaosIVTwitchPollProxyPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);

				var asyncResult = _pipe.BeginWaitForConnection((IAsyncResult result) => {
					GTA.Game.Console.Print("Pipe connected.");

					_pipe.EndWaitForConnection(result);

					_pipeReader = new StreamReader(_pipe);
					_pipeWriter = new StreamWriter(_pipe);
					_pipeWriter.AutoFlush = true;

					_pipeTimer.Tick += new EventHandler(OnElapsed);
					_pipeTimer.Interval = PIPE_INTERVAL;
					_pipeTimer.Start();
				}, null);
			}
			catch (Exception ex) {
				ChaosMain.ReportException(ex);
			}
		}

		public void Send(string message) {
			_pipeWriter.WriteLine(message);
		}

		private void OnElapsed(object s, EventArgs e) {
			try {
				if (_readPipeTask == null) {
					_readPipeTask = _pipeReader.ReadLineAsync();
				}
				else if (_readPipeTask.IsCompleted) {
					OnMessage?.Invoke(_readPipeTask.Result);
					_readPipeTask = null;
				}
			}
			catch (Exception ex) {
				ChaosMain.ReportException(ex);
				//Close();
			}
		}

		public bool IsConnected() {
			return _pipe.IsConnected;
		}

		public void Close() {
			_pipeTimer.Stop();
			_pipeReader.Close();
			_pipeWriter.Close();
			_pipe.Close();
		}
	}
}
