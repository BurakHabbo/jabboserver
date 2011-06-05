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
using JabboServer.Net;
using JabboServer.Messages;

namespace JabboServer
{
    internal class Engine
    {
        private static Config Config;

        private static WebSocket WebSocket;

        private static RoomManager RoomManager;
        private static string WelcomeMessage = "";

        internal static void Initialize()
        {
            Helpers.WriteLine("Credits to wichard for recoding the whole JabboServer!");

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
            Console.Title = GetConsoleTitle;
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
    }
}
