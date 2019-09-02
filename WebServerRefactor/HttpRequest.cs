using System;
using System.Net.Sockets;

namespace WebServerRefactor
{
    public class HttpRequest
    {
        public static void ParseRequest(string request, Socket conn)
        {
            int iStartPos = request.IndexOf("HTTP", 1);

            string HttpVersion = request.Substring(iStartPos, 8);
            Console.WriteLine($"HttpVersion : {HttpVersion}, iStartPos : {iStartPos}");
            Response.HttpVersion = HttpVersion;
            Response.conn = conn;
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
            StaticFileSystem fileSystem = new StaticFileSystem();
            fileSystem.GetLocalPath(sDirName, requestedFile);
            fileSystem.GetFileData(requestedFile);
        }
    }
}
