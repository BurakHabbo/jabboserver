using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabboServer.Core
{
    class Config
    {
        private Dictionary<string, string> Data;

        internal Config()
        {
            Clear();
        }

        internal void Initialize()
        {
            Clear();

            if (!File.Exists("Config\\config.ini"))
            {
                Helpers.WriteLine("Could not find: 'settings.ini' check your folder!");
                return;
            }

            foreach (string Line in File.ReadAllLines("Config\\config.ini"))
            {
                if (Line.Contains("=") && !Line.Contains("#"))
                {
                    Data.Add(Line.Split('=')[0], Line.Split('=')[1]);
                }
            }

            Helpers.WriteLine("Succesfully read settings!");
        }

        private void Clear()
        {
            Data = new Dictionary<string, string>();
        }

        internal string GetValue(string Key)
        {
            try { return Data[Key]; }
            catch { return ""; }
        }
    }
}
