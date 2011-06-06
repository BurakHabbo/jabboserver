using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using JabboServer.Core;
using JabboServer.Messages.WebSocket;
using JabboServer.Game;
using JabboServer.Game.Users;
using JabboServer.Util;

namespace JabboServer.Net.WebSocket
{
    class WebConnection
    {
        private readonly uint UserId;
        private Socket Sock;

        private readonly byte[] Buffer = new byte[1024];

        private Object SyncState = new Object();
        private AsyncCallback Async;

        private MessageHandler MessageHandler;

        internal User User;

        internal Boolean Handshake = false;

        internal byte[] GetBuffer
        {
            get
            {
                return Buffer;
            }
        }

        internal Boolean IsAlive
        {
            get
            {
                if (Sock == null)
                {
                    return false;
                }
                else if (Sock != null)
                {
                    return (Sock.Connected);
                }

                return false;
            }
        }

        internal string EndPoint
        {
            get
            {
                try
                {
                    return Sock.RemoteEndPoint.ToString();
                }
                catch { return ""; }
            }
        }

        internal WebConnection(uint Id, Socket mSock)
        {
            UserId = Id;
            Sock = mSock;
        }

        internal void Initialize()
        {
            Async = new AsyncCallback(Received);
            MessageHandler = new MessageHandler(this);
        }

        internal void Receive()
        {
            if (!IsAlive || Sock == null || !Sock.Connected)
            {
                return;
            }
            
            SyncState = Sock.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, Async, SyncState).AsyncState;
        }

        internal void Received(IAsyncResult Result)
        {
            if (!IsAlive || Result == null || Sock == null || !Sock.Connected)
            {
                return;
            }

            int ResultCount = 0;

            try
            {
                ResultCount = Sock.EndReceive(Result);
            }
            catch
            {
                Dispose();
                return;
            }

            if (ResultCount > 0)
            {
                byte[] Bytes = ByteUtil.ChompBytes(Buffer, 0, ResultCount);
                InvokeBytes(ref Bytes);
            }
            else
            {
                Dispose();
                return;
            }

            Receive();
        }

        private void InvokeBytes(ref byte[] Bytes)
        {
            if (Bytes != null)
            {
                Request Req = new Request(Encoding.ASCII.GetString(Bytes));

                MessageHandler.Invoke(Req);
            }
        }

        public static byte[] Warp(string Data)
        {
            byte[] Convert = Encoding.ASCII.GetBytes(Data.ToCharArray());
            byte[] Result = new byte[Convert.Length + 2];

            Result[0] = 0x00;

            for (var i = 0; i < Convert.Length; i++)
            {
                Result[i + 1] = Convert[i];
            }

            Result[Result.Length - 1] = 0xFF;

            return Result;
        }

        internal void SendResponse(Response Response)
        {
            if (!IsAlive)
            {
                return;
            }

            SendBytes(Encoding.ASCII.GetBytes(Response.ToString()), true);
        }

        internal void SendBytes(byte[] Bytes, bool WarpIt)
        {
            if (!IsAlive)
            {
                return;
            }

            if (WarpIt)
            {
                Sock.Send(Warp(Encoding.ASCII.GetString(Bytes)));
            }
            else
            {
                Sock.Send(Bytes);
            }
        }

        internal void Dispose()
        {
            if (!IsAlive)
            {
                return;
            }

            if (User != null)
            {
                if (User.InRoom)
                {
                    User.GetRoom().LeaveRoom(User);
                }
            }

            Helpers.WriteLine("Disconnection: " + EndPoint);

            Async = null;
            SyncState = null;

            Sock.Shutdown(SocketShutdown.Both);
            Sock = null;

            Engine.GetWebSocket().GetFactory().RemoveConnection(GetUserId());
        }

        internal uint GetUserId()
        {
            return UserId;
        }
    }
}
