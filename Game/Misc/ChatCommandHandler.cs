using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JabboServer.Game.Users;

namespace JabboServer.Game.Misc
{
    class ChatCommandHandler
    {
        internal static bool Parse(string Message, User User)
        {
            if (User == null)
            {
                return false;
            }

            Message = Message.Replace(":", "");

            var Params = Message.Split(' ').ToList();

            string Command = Params[0];

            switch (Command)
            {
                case "ha":
                case "hotelalert":
                    if (User.Rank > 5)
                    {
                        Engine.GetWebSocket().GetFactory().BroadcastHotelAlert(Message.Substring(3));
                        return true;
                    }
                    break;

                case "savechatlogs":
                    if (User.Rank > 5)
                    {
                        Engine.GetRoomManager().GetChatlogManager().Save();
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
