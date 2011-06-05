using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabboServer.Core
{
    internal class Helpers
    {
        internal static void WriteLine(string Msg)
        {
            DateTime Time = DateTime.Now;
            
            Engine.GetInterface().WriteLine(Time.ToShortDateString() + " " + Time.ToLongTimeString() + " => " + Msg);
        }

        internal static void WriteLine(string Msg, bool EnableTime)
        {
            DateTime Time = DateTime.Now;

            if (EnableTime)
            {
                Engine.GetInterface().WriteLine(Time.ToShortDateString() + " " + Time.ToLongTimeString() + " => " + Msg);
            }
            else
            {
                Engine.GetInterface().WriteLine(Msg);
            }
        }

        internal static void Clear()
        {
            Engine.GetInterface().Clear();
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
