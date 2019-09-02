using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WebServerRefactor
{
    public class WebServer
    {
        private TcpListener _listener;
        private int _port = 5555;
        private IPAddress _localAddr = IPAddress.Parse("127.0.0.1");

        public WebServer()
        {
            try
            {
                _listener = new TcpListener(_localAddr, _port);
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
}
