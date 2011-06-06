using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JabboServer.Core;
using JabboServer.Net.WebSocket;
using JabboServer.Game.Users;
using JabboServer.Game.Rooms;
using JabboServer.Game.Rooms.Pathfinding;

namespace JabboServer.Messages.WebSocket
{
    partial class MessageHandler
    {
        private WebConnection Connection;
        private Dictionary<string, Invoker> Invokers;

        private Request Request;
        private Response Response;

        internal delegate void Invoker();

        internal Response GetResponse()
        {
            return Response;
        }

        internal MessageHandler(WebConnection Connection)
        {
            if (Connection == null)
            {
                return;
            }

            this.Connection = Connection;
            this.Invokers = new Dictionary<string, Invoker>();

            this.Request = new Request("");
            this.Response = new Response();

            this.Register();
        }

        internal void Invoke(Request Req)
        {
            if (Req == null || Req.GetMessageId == "" || Req.GetMessageId == null)
            {
                return;
            }

            if (!Invokers.ContainsKey(Req.GetMessageId))
            {
                return;
            }

            Request = Req;
            Response = new Response();

            Invokers[Req.GetMessageId].Invoke();
        }

        internal void Register()
        {
            Invokers["requestname"] = RequestName;
            Invokers["handshake"] = HandShake;
            Invokers["enterroom"] = EnterRoom;
            Invokers["leaveroom"] = LeaveRoom;
            Invokers["requestusers"] = RequestUsers;
            Invokers["imove"] = Move;
            Invokers["italk"] = Talk;
        }

        internal void SendResponse()
        {
            if (Connection.IsAlive)
            {
                Connection.SendResponse(Response);
            }
        }
    }

    partial class MessageHandler
    {
        internal void RequestName()
        {
            string Username = Request.PopString();
            string FixedUsername = Helpers.Filter(Username);

            GetResponse().Init("changeName");
            GetResponse().AppendObject("ok", "/");
            GetResponse().AppendObject(Connection.GetUserId());
            SendResponse();

            Connection.User = new User(Connection.GetUserId(), Connection, FixedUsername);

            Helpers.WriteLine("[" + Connection.GetUserId() + "] - RequestName()");

            if (Engine.HasWelcomeMessage)
            {
                Connection.SendResponse(Engine.GetWelcomeMessage);
            }
        }

        internal void HandShake()
        {
            if (!Connection.Handshake)
            {
                HandShake Hs = new HandShake(Connection.GetBuffer);
                Connection.SendBytes(Hs.GetComputedBytes(), false);

                Connection.Handshake = true;

                Helpers.WriteLine("[" + Connection.GetUserId() + "] - HandShake()");
            }
        }

        internal void EnterRoom()
        {
            uint RoomId = Request.PopUInt32();

            Room Room = Engine.GetRoomManager().GetRoom(RoomId);

            if (Connection.User.InRoom)
            {
                Connection.User.GetRoom().LeaveRoom(Connection.User);
                Helpers.WriteLine("[" + Connection.GetUserId() + "] - LeaveOldRoom()");
            }

            if (Room != null)
            {
                Room.EnterRoom(Connection.User);
                Helpers.WriteLine("[" + Connection.GetUserId() + "] - EnterRoom()");
            }
        }

        internal void LeaveRoom()
        {
            if (Connection.User.InRoom)
            {
                Connection.User.GetRoom().LeaveRoom(Connection.User);
                Helpers.WriteLine("[" + Connection.GetUserId() + "] - LeaveRoom()");
            }
        }

        internal void RequestUsers()
        {
            Room Room = Connection.User.GetRoom();

            if (Room == null)
            {
                return;
            }

            foreach (User User in Room.Users)
            {
                if (User != Connection.User && User.GetRoom().Id == Room.Id)
                {
                    Connection.SendResponse(User.Serialize());
                }
            }

            Helpers.WriteLine("[" + Connection.GetUserId() + "] - RequestUsers()");
        }

        internal void Move()
        {
            int X = Request.PopInt32();
            int Y = Request.PopInt32();

            Coord newCoord = new Coord(X, Y);

            Connection.User.MoveTo(newCoord);

            Helpers.WriteLine("[" + Connection.GetUserId() + "] - Move()");
        }

        internal void Talk()
        {
            string Type = Request.PopString();
            string Message = Request.PopString();

            Connection.User.Chat(Message, Type);

            Helpers.WriteLine("[" + Connection.GetUserId() + "] - Talk()");
        }
    }
}
