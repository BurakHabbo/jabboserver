using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using JabboServer.Core;
using JabboServer.Game;

namespace JabboServer
{
    internal class Program
    {
        internal static Interface Interface;

        internal static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Interface = new Interface();

                Application.Run(Interface);
            }
            catch (Exception e)
            {
                Helpers.LogToFile(Application.StartupPath + "\\Logs\\errorlog.txt", e.ToString());
            }
        }

        internal static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            string Error = args.ExceptionObject.ToString();

            Helpers.LogToFile(Application.StartupPath + "\\Logs\\errorlog.txt", Error);
            Helpers.WriteLine(Error);
        }
    }
}