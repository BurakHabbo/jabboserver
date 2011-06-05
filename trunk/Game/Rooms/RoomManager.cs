using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JabboServer.Game.Rooms.Chatlogs;

namespace JabboServer.Game.Rooms
{
    class RoomManager
    {
        private ChatlogManager ChatlogManager;
        private List<Room> Rooms;

        internal int RoomCount()
        {
            return Rooms.Count;
        }

        internal RoomManager()
        {
            ChatlogManager = new ChatlogManager();
            Rooms = new List<Room>();
        }

        internal void InitRooms()
        {
            for (int i = 0; i < 4; i++)
            {
                Rooms.Add(new Room((uint)i));
            }
        }

        internal void LoadRoom(uint Id)
        {
            // todo :d

            Engine.UpdateTitle();
        }

        internal void UnloadRoom(uint Id)
        {
            // todo :d

            Engine.UpdateTitle();
        }

        internal Room GetRoom(uint Id)
        {
            try { return Rooms[(int)Id]; }
            catch { return null; }
        }

        internal ChatlogManager GetChatlogManager()
        {
            return ChatlogManager;
        }

        internal void Dispose()
        {
            foreach (Room Room in Rooms)
            {
                Room.Dispose();
            }

            Rooms = null;
        }
    }
}
