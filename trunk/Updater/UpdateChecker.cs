using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using JabboServer.Core;

namespace JabboServer.Updater
{
    class UpdateChecker
    {
        internal static bool NeedsUpdate()
        {
            string ThisVersion = Helpers.GetContentsFromFile("version.txt");
            string CurrentVersion = "";

            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://jabboserver.googlecode.com/svn/trunk/bin/Debug/version.txt");
                myRequest.Method = "GET";
                myRequest.KeepAlive = false;

                if (myRequest.GetResponse() == null)
                {
                    return false;
                }

                WebResponse myResponse = myRequest.GetResponse();

                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                CurrentVersion = sr.ReadToEnd();
                sr.Close();

                myResponse.Close();
            }
            catch (Exception) { return false; }

            if (CurrentVersion != ThisVersion)
            {
                return true;
            }

            return false;
        }
    }
}
