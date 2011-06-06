using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using JabboServer.Core;
using JabboServer.Game;
using JabboServer.Game.Users;
using JabboServer.Game.Rooms;
using JabboServer.Game.Rooms.Chatlogs;
using JabboServer.Net.WebSocket;
using JabboServer.Messages.WebSocket;

namespace JabboServer
{
    internal class Engine
    {
        private static Interface Interface;
        private static Config Config;

        private static WebSocket WebSocket;

        private static RoomManager RoomManager;
        private static string WelcomeMessage = "";

        internal static void Initialize(Interface i)
        {
            Interface = i;

            Helpers.WriteLine("Credits to:", false);
            Helpers.WriteLine("- PEjump2 for coding the base.", false);
            Helpers.WriteLine("- joopie for coding the base of the Packet structure.", false);
            Helpers.WriteLine("- TopErwin with helping to fix exploits.", false);
            Helpers.WriteLine("- wichard for recoding the whole JabboServer.", false);
            Helpers.WriteLine(Environment.NewLine, false);

            Config = new Config();
            Config.Initialize();

            WelcomeMessage = Config.GetValue("hotelalert.text");

            WebSocket = new WebSocket(Config.GetValue("websocket.ip"), int.Parse(Config.GetValue("websocket.port")), int.Parse(Config.GetValue("websocket.max.connections")));
            WebSocket.Request();

            RoomManager = new RoomManager();
            RoomManager.InitRooms();

            Helpers.WriteLine("Succesfully Initialized JabboServer.");

            UpdateTitle();
        }

        internal static void UpdateTitle()
        {
            Interface.SetTitle(GetConsoleTitle);
        }

        internal static void Dispose()
        {
            Config = null;
            Helpers.WriteLine("Destroyed Config.");

            WebSocket.Dispose();
            WebSocket = null;
            Helpers.WriteLine("Destroyed WebSocket.");

            RoomManager.GetChatlogManager().Save();

            RoomManager.Dispose();
            RoomManager = null;
            Helpers.WriteLine("Destroyed RoomManager.");

            Environment.Exit(0);
        }

        internal static string GetConsoleTitle
        {
            get
            {
                return "JabboServer [C#] - Users online: " + GetWebSocket().ConnectionCount + " - Rooms loaded: " + RoomManager.RoomCount();
            }
        }

        internal static Response GetWelcomeMessage
        {
            get
            {
                Response Response = new Response();

                Response.Init("hotelAlert");
                Response.AppendObject(WelcomeMessage);

                return Response;
            }
        }

        internal static bool HasWelcomeMessage
        {
            get
            {
                return (WelcomeMessage != "");
            }
        }

        internal static Config GetConfig()
        {
            return Config;
        }

        internal static WebSocket GetWebSocket()
        {
            return WebSocket;
        }

        internal static RoomManager GetRoomManager()
        {
            return RoomManager;
        }

        internal static Interface GetInterface()
        {
            return Interface;
        }
    }
}
