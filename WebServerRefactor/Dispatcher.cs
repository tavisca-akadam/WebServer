using System;
using System.Net.Sockets;
using System.Text;

namespace WebServerRefactor
{
    public class Dispatcher
    {
        public static void GenerateResponse()
        {
            string responseBuffer = "";
            if (Response.MimeType.Length == 0)
                Response.MimeType = "text/html";
            Console.WriteLine($"Response.MimeType : {Response.MimeType}");
            responseBuffer += Response.HttpVersion + Response.StatusCode + "\r\n";
            responseBuffer += "Server: C# Http Server\r\n";
            responseBuffer += "Content-Type: " + Response.MimeType + "\r\n";
            responseBuffer += "Accept-Ranges: bytes\r\n";
            responseBuffer += "Content-Length: " + Response.ResponseLength + "\r\n\r\n";

            byte[] sendData = Encoding.ASCII.GetBytes(responseBuffer);
            Console.WriteLine("Total Bytes : " + Response.ResponseLength.ToString());
            int numBytes = 0;
            Socket socket = Response.conn;
            try
            {
                if(socket.Connected)
                {
                    if ((numBytes = socket.Send(sendData, sendData.Length, 0)) == -1)
                    {
                        Console.WriteLine("Socket Error cannot Send Packet");
                    }
                    else
                    {
                        Console.WriteLine("No. of bytes send {0}", numBytes);
                    }
                }
                else
                    Console.WriteLine("Disconnected");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred : {0} ", e);
            }
        }
        public static void SendToBrowser(byte[] sendData)
        {
            int numBytes = 0;
            Socket socket = Response.conn;
            try
            {
                if (socket.Connected)
                {
                    if ((numBytes = socket.Send(sendData, sendData.Length, 0)) == -1)
                    {
                        Console.WriteLine("Socket Error cannot Send Packet");
                    }
                    else
                    {
                        Console.WriteLine("No. of bytes send {0}", numBytes);
                    }
                }
                else
                    Console.WriteLine("Disconnected");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred : {0} ", e);
            }
        }
    }
}
