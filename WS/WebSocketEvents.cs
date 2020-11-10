using System;

namespace ChaosIV.WS
{
    public class WebSocketEvents {
        public Action<ArraySegment<byte>> OnMessage;
        public Action OnClientConnected;
        public Action OnClientDisconnected;
    }
}