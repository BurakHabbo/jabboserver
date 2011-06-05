using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabboServer.Game.Rooms.Chatlogs
{
    class ChatMessage
    {
        public string Username;
        public string Message;
        public string Time;

        public ChatMessage(string mUsername, string mMessage, string mTime)
        {
            this.Username = mUsername;
            this.Message = mMessage;
            this.Time = mTime;
        }
    }
}
