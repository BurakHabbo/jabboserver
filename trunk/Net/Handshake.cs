using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace JabboServer.Net
{
    public class HandShake
    {
        // 0x21 -- 0x2f !\"#$%&'()*+,-.
        // 0x3a -- 0x7e :;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}
        private const string handShakeKeyChars = "!\"#$%&'()*+,-.:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}";
        private readonly Regex hostExp = new Regex("^(?<protocol>ws://|ws:///)(?<host>.+)");
        /// <summary>
        /// HandShake Constructor for websocket server
        /// </summary>
        /// <param name="handShakeBytes">the bytes from client</param>
        public HandShake(byte[] handShakeBytes)
        {
            this.Initialize(handShakeBytes);
        }

        /// <summary>
        /// HandShake Constructor for websocket client
        /// </summary>
        /// <param name="host">
        /// |Host| field is used to protect against DNS rebinding attacks and to
        /// allow multiple domains to be served from one IP address.
        /// </param>
        /// <param name="origin">
        /// The |Origin| field is used to protect against unauthorized cross-
        /// origin use of a WebSocket server by scripts using the |WebSocket| API
        /// in a Web browser. 
        /// </param>
        public HandShake(string host, string origin)
        {
            // initialized == false
            MatchCollection mc = hostExp.Matches(host);

            if (mc.Count != 0)
            {
                GroupCollection gc = mc[0].Groups;

                string protocol = gc["protocol"].Value;
                this.IsWSS = protocol.IndexOf("wss") == -1 ? false : true;

                this.Host = gc["host"].Value;
                this.Origin = origin;
            }
            else
            {
                throw new ArgumentException("The format of host is not valid.");
            }
        }

        public HandShake(string host, string origin, string protocol) : this(host, origin)
        {
            this.SecWebSocketProtocol = protocol;
        }

        public string Upgrade { get; set; }

        public string Connection { get; set; }

        public string SecWebSocketProtocol { get; set; }

        public string Host { get; set; }

        public bool IsWSS { get; set; }

        public string Origin { get; set; }

        public string SecWebSocketKey1 { get; set; }

        public string SecWebSocketKey2 { get; set; }

        public byte[] Key3 { get; set; }

        private bool initialized;

        public void Initialize(byte[] handShakeBytes)
        {
            string dataStr = Encoding.UTF8.GetString(handShakeBytes);

            StringReader reader = new StringReader(dataStr);
            // read method and version
            string eachLine = reader.ReadLine();

            // read headers
            while (!string.IsNullOrEmpty(eachLine = reader.ReadLine()))
            {
                // first :
                int pos = eachLine.IndexOf(':');
                string key = eachLine.Substring(0, pos).Trim();
                string value = eachLine.Substring(pos + 1, eachLine.Length - pos - 1).Trim();

                switch (key.ToUpper())
                {
                    case "UPGRADE":
                        this.Upgrade = value;
                        break;
                    case "CONNECTION":
                        this.Connection = value;
                        break;
                    case "HOST":
                        this.Host = value;
                        break;
                    case "ORIGIN":
                        this.Origin = value;
                        break;
                    case "SEC-WEBSOCKET-PROTOCOL":
                        this.SecWebSocketProtocol = value;
                        break;
                    case "SEC-WEBSOCKET-KEY1":
                        this.SecWebSocketKey1 = value;
                        break;
                    case "SEC-WEBSOCKET-KEY2":
                        this.SecWebSocketKey2 = value;
                        break;
                }
            }

            int lastLF = dataStr.LastIndexOf('\n');
            this.Key3 = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                this.Key3[i] = handShakeBytes[lastLF + i + 1];
            }

            this.initialized = true;
        }

        private UInt32 GetComputedNum(string key)
        {
            int spaceNum = 0;

            StringBuilder numBuilder = new StringBuilder();

            foreach (char c in key)
            {
                if (char.IsDigit(c))
                {
                    numBuilder.Append(c);
                }
                else if (char.IsWhiteSpace(c))
                {
                    spaceNum++;
                }
            }

            return Convert.ToUInt32(Convert.ToInt64(numBuilder.ToString()) / spaceNum);
        }

        public byte[] GetComputedBytes()
        {
            if (!this.initialized)
            {
                throw new ApplicationException("Before compute the bytes, please initialize the object.");
            }

            UInt32 k1 = this.GetComputedNum(this.SecWebSocketKey1);
            UInt32 k2 = this.GetComputedNum(this.SecWebSocketKey2);

            byte[] k1s = BitConverter.GetBytes(k1);
            byte[] k2s = BitConverter.GetBytes(k2);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(k1s);
                Array.Reverse(k2s);
            }

            byte[] answerBytes = new byte[16];

            Array.Copy(k1s, 0, answerBytes, 0, k1s.Length);
            Array.Copy(k2s, 0, answerBytes, k2s.Length, k2s.Length);
            Array.Copy(this.Key3, 0, answerBytes, k2s.Length + k1s.Length, this.Key3.Length);

            byte[] changedBytes = MD5.Create().ComputeHash(answerBytes);

            // the host is ws or wss
            // but wss is not implemented in this version
            string location = "ws://" + this.Host + (this.Host.EndsWith("/") ? "" : "/");

            string sendStr =
                "HTTP/1.1 101 WebSocket Protocol Handshake\r\n" +
                "Upgrade:WebSocket\r\n" +
                "Connection:Upgrade\r\n" +
                "Sec-WebSocket-Origin: " + this.Origin + "\r\n" +
                "Sec-WebSocket-Location: " + location + "\r\n";

            StringBuilder responseBuilder = new StringBuilder(sendStr);

            if (!string.IsNullOrEmpty(this.SecWebSocketProtocol))
            {
                responseBuilder.AppendFormat("Sec-WebSocket-Protocol:{0}\r\n", this.SecWebSocketProtocol);
            }

            responseBuilder.Append("\r\n");

            byte[] headerBytes = Encoding.UTF8.GetBytes(responseBuilder.ToString());

            int headerBytesLength = headerBytes.Length;
            int changeBytesLength = changedBytes.Length;

            byte[] response = new byte[headerBytesLength + changeBytesLength];

            Array.Copy(headerBytes, 0, response, 0, headerBytesLength);
            Array.Copy(changedBytes, 0, response, headerBytesLength, changedBytes.Length);

            return response;
        }

        public byte[] OpenHandShake()
        {
            Random Generator = new Random();

            // caculate the key1/key2/key3
            int key1SpaceNum = Generator.Next(1, 12);
            int key2SpaceNum = Generator.Next(1, 12);

            // max = 4,294,967,295 / spaceNum
            // 4,294,967,295 is the max num of 32bit int
            uint maxNum = 4294967295;
            uint base1Num = (uint)(Generator.NextDouble() * (maxNum / key1SpaceNum));
            uint base2Num = (uint)(Generator.NextDouble() * (maxNum / key2SpaceNum));

            string key1 = Convert.ToString(base1Num * key1SpaceNum);
            string key2 = Convert.ToString(base2Num * key2SpaceNum);

            int random1CharsNum = Generator.Next(1, 12);
            int random2CharsNum = Generator.Next(1, 12);

            StringBuilder key1Builder = new StringBuilder(key1);
            StringBuilder key2Builder = new StringBuilder(key2);

            // insert the random character in random position for random times
            for (int cnt = 0; cnt < random1CharsNum; cnt++)
            {
                char c = handShakeKeyChars[Generator.Next(0, handShakeKeyChars.Length - 1)];
                int insertPos = Generator.Next(0, key1.Length - 1);
                key1Builder.Insert(insertPos, c);
            }

            for (int cnt = 0; cnt < random2CharsNum; cnt++)
            {
                char c = handShakeKeyChars[Generator.Next(0, handShakeKeyChars.Length - 1)];
                int insertPos = Generator.Next(0, key2.Length - 1);
                key2Builder.Insert(insertPos, c);
            }

            key1 = key1Builder.ToString();
            key2 = key2Builder.ToString();

            // insert space
            for (int cnt = 0; cnt < key1SpaceNum; cnt++)
            {
                int insertPos = Generator.Next(0, key1.Length - 1);
                key1Builder.Insert(insertPos, " ");
            }

            for (int cnt = 0; cnt < key1SpaceNum; cnt++)
            {
                int insertPos = Generator.Next(0, key2.Length - 1);
                key2Builder.Insert(insertPos, " ");
            }

            // the final key1 and key2
            this.SecWebSocketKey1 = key1 = key1Builder.ToString();
            this.SecWebSocketKey2 = key2 = key2Builder.ToString();

            // prepare for key3 from 64bit integer
            // and use the big-endian order
            byte[] key3Bytes = BitConverter.GetBytes(Generator.NextDouble() * UInt64.MaxValue);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(key3Bytes);
            }

            string openStr =
                "GET / HTTP/1.1\r\n" +
                "Upgrade: WebSocket\r\n" +
                "Connection: Upgrade\r\n" +
                "Host: " + this.Host + "\r\n" +
                "Origin: " + this.Origin + "\r\n" +
                "Sec-WebSocket-Key1: " + this.SecWebSocketKey1 + "\r\n" +
                "Sec-WebSocket-Key2: " + this.SecWebSocketKey2 + "\r\n";

            StringBuilder openStrBuilder = new StringBuilder(openStr);

            if (!string.IsNullOrEmpty(this.SecWebSocketProtocol))
            {
                openStrBuilder.AppendFormat("Sec-WebSocket-Protocol: {0}\r\n", this.SecWebSocketProtocol);
            }

            openStrBuilder.Append("\r\n");

            byte[] resultBytes = Encoding.UTF8.GetBytes(openStrBuilder.ToString());
            Array.Resize(ref resultBytes, resultBytes.Length + key3Bytes.Length);
            Array.Copy(key3Bytes, 0, resultBytes, resultBytes.Length - key3Bytes.Length, key3Bytes.Length);

            return resultBytes;
        }
    }
}