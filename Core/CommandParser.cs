using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabboServer.Core
{
    class CommandParser
    {
        internal static void Parse(string Line)
        {
            var Params = Line.Split(' ').ToList();

            switch (Params[0])
            {
                case "ha":
                    Engine.GetWebSocket().GetFactory().BroadcastHotelAlert(Line.Substring(3));
                    break;

                case "exit":
                case "close":
                case "dispose":
                case "destroy":
                    Engine.Dispose();
                    break;

                case "savechatlogs":
                    Engine.GetRoomManager().GetChatlogManager().Save();
                    break;
            }
        }
    }
}
