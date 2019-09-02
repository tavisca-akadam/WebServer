using System.Net.Sockets;

namespace WebServerRefactor
{
    public class Response
    {
        public static string HttpVersion { get; set; }
        public static string MimeType { get; set; }
        public static Socket conn { get; set; }
        public static int ResponseLength { get; set; }
        public static string StatusCode { get; set; }
        public static byte[] ResponseBody { get; set; }
    }
}
