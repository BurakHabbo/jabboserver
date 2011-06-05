using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

using JabboServer.Core;
using JabboServer.Messages;

namespace JabboServer.Net
{
    class WebSocket
    {
        private readonly int Backlog = 4;
        private readonly byte[] Buffer = new byte[1024];

        private Object SyncState = new Object();
        private AsyncCallback Async;
        private Socket WebSock;

        private Factory ConnectionFactory;

        internal int ConnectionCount
        {
            get
            {
                return GetFactory().ConnectionCount();
            }
        }

        internal Boolean IsAlive
        {
            get
            {
                return (WebSock != null);
            }
        }

        internal WebSocket(string IP, int PORT, int MAX)
        {
            IPAddress oIP;

            if (!IPAddress.TryParse(IP, out oIP))
            {
                Helpers.WriteLine("Invalid IP in config!");
                return;
            }

            EndPoint EP = new IPEndPoint(oIP, PORT);

            WebSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Async = new AsyncCallback(Received);
            ConnectionFactory = new Factory(MAX);

            try
            {
                WebSock.Bind(EP);
            }
            catch (Exception e)
            {
                Helpers.WriteLine(e.Message);
                return;
            }

            WebSock.Listen(Backlog);

            Helpers.WriteLine("WebSocket listening on: " + WebSock.LocalEndPoint);
        }

        internal void Dispose()
        {
            if (!IsAlive)
            {
                return;
            }

            GetFactory().Dispose();
            ConnectionFactory = null;

            Async = null;
            SyncState = null;

            WebSock = null;
        }

        internal void Request()
        {
            SyncState = WebSock.BeginAccept(Async, SyncState).AsyncState;
        }

        internal void Received(IAsyncResult Result)
        {
            if (!IsAlive)
            {
                Helpers.WriteLine("Warning! WebSocket died, he will not accept anymore!");
                return;
            }

            Socket GainedSocket = WebSock.EndAccept(Result);

            if (GainedSocket != null)
            {
                if (ConnectionFactory.HandleNewConnection(GainedSocket))
                {
                    Helpers.WriteLine("Accepted new connection: " + GainedSocket.RemoteEndPoint);
                }
            }

            Request();
        }

        internal Factory GetFactory()
        {
            return ConnectionFactory;
        }
    }

    class Factory
    {
        private uint UserId = 0;
        private Dictionary<uint, WebConnection> Connections;
        private readonly int MAX_CONNECTIONS_ALLOWED;
        private readonly Object _lock = new Object();

        internal uint GenerateUserId
        {
            get
            {
                lock (_lock)
                {
                    return UserId++;
                }
            }
        }

        internal int ConnectionCount()
        {
            return Connections.Count;
        }

        internal Factory(int MAX)
        {
            MAX_CONNECTIONS_ALLOWED = MAX;
            Connections = new Dictionary<uint, WebConnection>();
        }

        internal Boolean HandleNewConnection(Socket Sock)
        {
            if (Connections.Count == MAX_CONNECTIONS_ALLOWED)
            {
                Helpers.WriteLine("Warning! Max. connections reached!");
                return false;
            }

            if (Connections.Count >= MAX_CONNECTIONS_ALLOWED)
            {
                return false;
            }

            if (Sock == null || !Sock.Connected)
            {
                return false;
            }

            uint GeneratedId = GenerateUserId;

            WebConnection Connection = new WebConnection(GeneratedId, Sock);

            Connections.Add(Connection.GetUserId(), Connection);

            Connection.Initialize();
            Connection.Receive();

            Engine.UpdateTitle();

            return (Connection != null);
        }

        internal void BroadcastHotelAlert(string Alert)
        {
            Response Response = new Response();

            Response.Init("hotelAlert");
            Response.AppendObject(Alert);

            foreach (WebConnection Connection in Connections.Values.ToList())
            {
                Connection.SendResponse(Response);
            }
        }

        internal void Dispose()
        {
            foreach (WebConnection Connection in Connections.Values.ToList())
            {
                RemoveConnection(Connection.GetUserId());
            }

            Connections = null;
        }

        internal void RemoveConnection(uint UserId)
        {
            if (!Connections.ContainsKey(UserId))
            {
                return;
            }

            if (Connections.ContainsKey(UserId))
            {
                Connections[UserId].Dispose();
                Connections.Remove(UserId);
            }

            Engine.UpdateTitle();
        }
    }
}
