using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabboServer.Core
{
    internal class Helpers
    {
        internal static object LogLock;

        internal static void WriteLine(string Msg)
        {
            DateTime Time = DateTime.Now;
            LogLock = new Object();

            lock (LogLock)
            {
                Console.WriteLine(Time.ToShortDateString() + " " + Time.ToLongTimeString() + " => " + Msg);
            }

            GC.SuppressFinalize(LogLock);
        }

        internal static void WriteLine(string Msg, bool EnableTime)
        {
            DateTime Time = DateTime.Now;
            LogLock = new Object();

            lock (LogLock)
            {
                if (EnableTime)
                {
                    Console.WriteLine(Time.ToShortDateString() + " " + Time.ToLongTimeString() + " => " + Msg);
                }
                else
                {
                    Console.WriteLine(Msg);
                }
            }

            GC.SuppressFinalize(LogLock);
        }

        internal static void Clear()
        {
            LogLock = new Object();

            lock (LogLock)
            {
                Console.Clear();
            }

            GC.SuppressFinalize(LogLock);
        }

        internal static string Filter(string Msg)
        {
            string SafeMsg = Msg.Replace("<", "&lt;").Replace(">", "&gt;");

            return SafeMsg;
        }

        internal static void LogToFile(string filePath, string Data)
        {
            StreamWriter Log;

            if (!File.Exists(filePath))
            {
                Log = new StreamWriter(filePath);
            }
            else
            {
                Log = File.AppendText(filePath);
            }

            Log.WriteLine(Data.ToString());

            Log.Dispose();
            Log.Close();
        }
    }
}
