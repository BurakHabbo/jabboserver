using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JabboServer.Core;
using JabboServer.Net;
using JabboServer.Messages;
using JabboServer.Game.Misc;
using JabboServer.Game.Rooms;
using JabboServer.Game.Rooms.Pathfinding;

namespace JabboServer.Game.Users
{
    class User
    {
        private uint UserId;
        private WebConnection Socket;

        private string Username;
        private string Motto = "Motto sucks :D";

        private uint RoomId = 0;

        private Room Room;
        internal int Rank = 1;

        private Coord Coordinate = new Coord(-1, -1);

        internal int X
        {
            get
            {
                return Coordinate.X;
            }
        }

        internal int Y
        {
            get
            {
                return Coordinate.Y;
            }
        }

        internal Boolean InRoom
        {
            get
            {
                return (Room != null);
            }
        }

        internal Room GetRoom()
        {
            return Room;
        }

        internal WebConnection GetConnection()
        {
            return Socket;
        }

        internal User(uint Id, WebConnection Connection, string Name)
        {
            UserId = Id;
            Socket = Connection;
            Username = Name;
        }

        internal void Chat(string Message, string Type)
        {
            if (!InRoom)
            {
                return;
            }

            if (Message == "" || Message.Length < 1)
            {
                return;
            }

            if (Message.StartsWith(":") && ChatCommandHandler.Parse(Message, this))
            {
                return;
            }

            string FilteredMessage = Helpers.Filter(Message);

            Engine.GetRoomManager().GetChatlogManager().AddChatMessage(Username, FilteredMessage, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            Response Response = new Response();

            Response.Init("talk");
            Response.AppendObject(UserId, "/");
            Response.AppendObject(Type, "/");
            Response.AppendObject(FilteredMessage);

            Room.SendResponse(Response, this);
        }

        internal void MoveTo(Coord Movement)
        {
            if (!InRoom)
            {
                return;
            }

            Coordinate = Movement;

            Response Response = new Response();

            Response.Init("move");
            Response.AppendObject(UserId, "/");
            Response.AppendObject(Movement.X, "/");
            Response.AppendObject(Movement.Y);

            Room.SendResponse(Response, this);
        }

        internal void SendResponse(Response Response)
        {
            if (!Socket.IsAlive)
            {
                return;
            }

            Socket.SendResponse(Response);
        }

        internal Response EnterRoomSerialize(Room Room)
        {
            this.Room = Room;
            this.RoomId = Room.Id;

            Response EnterRoom = new Response();

            EnterRoom.Init("er");
            EnterRoom.AppendObject(UserId, "/");
            EnterRoom.AppendObject(Username, "/");
            EnterRoom.AppendObject(Motto);

            return EnterRoom;
        }

        internal Response LeaveRoomSerialize()
        {
            this.Room = null;
            this.RoomId = 0;

            Response LeaveRoom = new Response();

            LeaveRoom.Init("lr");
            LeaveRoom.AppendObject(UserId);

            return LeaveRoom;
        }

        internal Response Serialize()
        {
            Response Info = new Response();

            Info.Init("ru");
            Info.AppendObject(UserId, "|");
            Info.AppendObject(Username, "|");
            Info.AppendObject(Motto, "|");

            if (X != -1 && Y != -1)
            {
                Info.AppendObject(X, "|");
                Info.AppendObject(Y);
            }

            return Info;
        }
    }
}
