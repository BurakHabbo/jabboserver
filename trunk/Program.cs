using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JabboServer.Core;
using JabboServer.Game;

namespace JabboServer
{
    internal class Program
    {
        internal static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            try
            {
                Engine.Initialize();

                Console.ReadKey(true);

                while (true)
                {
                    CommandParser.Parse(Console.ReadLine());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey(true);
            }
        }

        internal static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            string Error = args.ExceptionObject.ToString();

            Helpers.LogToFile("Logs\\errorlog.txt", Error);
            Helpers.WriteLine(Error);

            Console.ReadKey(true);
        }
    }
}