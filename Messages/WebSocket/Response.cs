using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabboServer.Messages.WebSocket
{
    class Response
    {
        private StringBuilder Builder = new StringBuilder("");

        internal Response() { }

        internal void Init(string Type)
        {
            Builder.Append(Convert.ToChar(0) + Type + "/");
        }

        internal void AppendObject(Object e)
        {
            AppendObject(e, "");
        }

        internal void AppendObject(Object e, string Break)
        {
            Builder.Append(e.ToString());
            Builder.Append(Break);
        }

        public override string ToString()
        {
            return Builder.ToString();
        }
    }
}
