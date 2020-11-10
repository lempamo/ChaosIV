﻿using System.Net.WebSockets;

 namespace ChaosIV.WS
{
    public class ConnectedClient
    {
        public ConnectedClient(int socketId, WebSocket socket)
        {
            SocketId = socketId;
            Socket = socket;
        }

        public int SocketId { get; private set; }

        public WebSocket Socket { get; private set; }
    }
}
