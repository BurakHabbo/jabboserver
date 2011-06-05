using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JabboServer.Core;

namespace JabboServer.Game.Rooms.Chatlogs
{
    internal class ChatlogManager
    {
        private List<ChatMessage> ChatMessages;

        internal ChatlogManager()
        {
            this.ChatMessages = new List<ChatMessage>();
        }

        internal void AddChatMessage(string Username, string Message, string Time)
        {
            ChatMessages.Add(new ChatMessage(Username, Message, Time));
        }

        internal void Save()
        {
            if (ChatMessages.Count > 0)
            {
                string LogData = "";

                foreach (ChatMessage ChatM in ChatMessages)
                {
                    LogData += "[" + ChatM.Time + "] " + ChatM.Username + " says: " + ChatM.Message;
                    LogData += Environment.NewLine;
                }

                Helpers.LogToFile("Logs\\chatlog.txt", LogData);
            }

            ChatMessages.Clear();
        }
    }
}
