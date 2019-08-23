using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WebServerRefactor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            WebServer webServer = new WebServer();
            Console.ReadKey();
        }
    }
    public class WebServer
    {
        private TcpListener _listener;
        private int port = 5555;

        public WebServer()
        {
            try
            {
                _listener = new TcpListener(port);
                _listener.Start();
                Console.WriteLine("Web Server Running Press CTRL + C to quit.....");

                Thread deamon = new Thread(new ThreadStart(StartListen));
                deamon.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine("An Exception Occurred while Listening :" + e.ToString());
            }
        }
        public void StartListen()
        {
            while(true)
            {
                Socket socket = _listener.AcceptSocket();
                Console.WriteLine("Socket Type :" + socket.SocketType);
                if(socket.Connected)
                {
                    Console.WriteLine("Client Connected \n Client IP " + socket.RemoteEndPoint);
                    Parser.TryParse(socket);
                }
            }
        }
    }
    public class Parser
    {

        public static void TryParse(Socket conn)
        {
            byte[] receivedBuffer = new byte[1024];
            int messageLength = conn.Receive(receivedBuffer, receivedBuffer.Length, 0);
            string messageBuffer = Encoding.ASCII.GetString(receivedBuffer);

            Console.WriteLine($"messageLength : {messageLength}, messageBuffer : {messageBuffer}");

        }
    }
}
