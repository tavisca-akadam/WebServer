using System;
using System.Net;
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
    public class Parser
    {

        public static void TryParse(Socket conn)
        {
            byte[] receivedBuffer = new byte[1024];
            int requestLength = conn.Receive(receivedBuffer, receivedBuffer.Length, 0);
            string request = Encoding.ASCII.GetString(receivedBuffer);

            Console.WriteLine($"messageLength : {requestLength}, messageBuffer : {request}");

            if(request.Substring(0, 3) == "GET")
            {
                HttpRequest.ParseRequest(request);
            }
            else
            {
                Console.WriteLine("Only Get Method is Supported");
                conn.Close();
                return;
            }

        }
    }

    public class HttpRequest
    {
        public static void ParseRequest(string request)
        {
            int iStartPos = request.IndexOf("HTTP", 1);

            string HttpVersion = request.Substring(iStartPos, 8);
            Console.WriteLine($"HttpVersion : {HttpVersion}, iStartPos : {iStartPos}");

            string sRequest = request.Substring(0, iStartPos - 1);
            sRequest.Replace("\\", "/");
            
            if ((sRequest.IndexOf(".") < 1) && (!sRequest.EndsWith("/")))
            {
                sRequest = sRequest + "/";
            }
            Console.WriteLine($"sRequest : {sRequest}");
            iStartPos = sRequest.LastIndexOf("/") + 1;
            string requestedFile = sRequest.Substring(iStartPos);
            string sDirName = sRequest.Substring(sRequest.IndexOf("/"), sRequest.LastIndexOf("/") - 3);
            Console.WriteLine($"requestedFile : {requestedFile}, sDirName : {sDirName} ");
        }
    }

    public interface IFileSystem
    {
        string GetLocalPath(string rootDir, string localDir);
        string GetTheDefaultFileName(string localDirectory);
    }

    public class StaticFileSystem : IFileSystem
    {
        public string GetLocalPath(string rootDir, string localDir)
        {
            throw new NotImplementedException();
        }

        public string GetTheDefaultFileName(string localDirectory)
        {
            throw new NotImplementedException();
        }
    }
}
