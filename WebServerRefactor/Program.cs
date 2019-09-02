using System;

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
}
