using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JabboServer.Messages
{
    class Request
    {
        private string q;
        private List<string> Data;

        private string MessageId;
        private int Pointer = 0;

        internal string GetMessageId
        {
            get
            {
                return MessageId.ToLower();
            }
        }

        internal Request(string R)
        {
            if (R.ToLower().Contains("websocket"))
            {
                MessageId = "handshake";
                this.q = R;
            }
            else
            {
                this.q = R.Replace(Convert.ToChar(0).ToString(), "").Replace("?", "");
                GenerateIndex();
            }
        }

        private void GenerateIndex()
        {
            Data = new List<string>();

            foreach (string i in q.Split('/').ToList()) // First / because of MessageId
            {
                Data.Add(i);
            }

            foreach (string i in q.Split('|').ToList())
            {
                Data.Add(i);
            }

            MessageId = (string)PopObject();
        }

        internal object PopObject()
        {
            object e = null;

            e = Data[Pointer];

            Pointer++;

            return e;
        }

        internal string PopString()
        {
            try { return Convert.ToString(PopObject()); }
            catch (FormatException) { return null; }
        }

        internal uint PopUInt32()
        {
            try { return Convert.ToUInt32(PopObject()); }
            catch (FormatException) { return 0; }
        }

        internal int PopInt32()
        {
            try { return Convert.ToInt32(PopObject()); }
            catch (FormatException) { return 0; }
        }

        internal bool PopBoolean()
        {
            return (PopInt32() == 1);
        }

        internal byte[] PopBytes()
        {
            return Encoding.ASCII.GetBytes(PopString());
        }

        public override string ToString()
        {
            return q;
        }
    }
}
