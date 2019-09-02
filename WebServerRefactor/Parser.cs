using System;
using System.Net.Sockets;
using System.Text;

namespace WebServerRefactor
{
    public class Parser
    {
        public static string MethodType { get; private set; }
        public static string UrlPath { get; private set; }
        public static string Body { get; private set; }
        public static void TryParse(Socket conn)
        {
            byte[] receivedBuffer = new byte[1024];
            int requestLength = conn.Receive(receivedBuffer, receivedBuffer.Length, 0);
            string request = Encoding.ASCII.GetString(receivedBuffer, 0, requestLength);
            string[] streamFactors = request.Split('\n');
            string[] url = streamFactors[0].Split(' ');

            MethodType = url[0];
            UrlPath = url[1];


            Console.WriteLine($"messageLength : {requestLength}, messageBuffer : {request}");
         
            if(request.Substring(0, 3) == "GET")
            {
                HttpRequest.ParseRequest(request, conn);
            }
            else if(MethodType == "POST")
            {
                Console.WriteLine("In POST");
                Body = request.Substring(request.IndexOf("{"), (request.IndexOf("}") - request.IndexOf("{") + 1));
                string[] splitPath =UrlPath.Split("/");
                if(splitPath[1] == "api")
                {
                    var api = new RestApi(conn, "year", Body);
                    api.Response();
                }
                else
                {
                    Console.WriteLine("Invalid API request");
                    conn.Close();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Only Get Method is Supported");
                conn.Close();   
                return;
            }

        }
    }
}
