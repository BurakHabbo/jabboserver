using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JabboServer.Net.WebSocket;
using JabboServer.Messages.WebSocket;
using JabboServer.Game.Users;

namespace JabboServer.Game.Rooms
{
    class Room
    {
        internal readonly uint Id;
        internal List<User> Users;

        internal Room(uint Id)
        {
            this.Id = Id;
            Users = new List<User>();
        }

        internal void EnterRoom(User User)
        {
            SendResponse(User.EnterRoomSerialize(this));

            Users.Add(User);
        }

        internal void LeaveRoom(User User)
        {
            Users.Remove(User);

            SendResponse(User.LeaveRoomSerialize());
        }

        internal void SendResponse(Response Response)
        {
            foreach (User User in Users)
            {
                if (User != null)
                {
                    if (User.GetConnection() != null)
                    {
                        User.SendResponse(Response);
                    }
                }
            }
        }

        internal void SendResponse(Response Response, User Usr)
        {
            foreach (User User in Users)
            {
                if (User != null && User != Usr)
                {
                    if (User.GetConnection() != null)
                    {
                        User.SendResponse(Response);
                    }
                }
            }
        }

        internal void Dispose()
        {
            foreach (User User in Users)
            {
                LeaveRoom(User);
            }

            Users = null;
        }
    }
}
