using System;
using System.IO;
using System.Text;

namespace WebServerRefactor
{
    public class StaticFileSystem : IFileSystem
    {
        string PhysicalPath = "";
        string ErrorPage = "error\\404.html";
        public StaticFileSystem()
        {
            RootDirectory = "C:\\Users\\ankadam\\source\\repos\\WebServerRefactor\\WebServerRefactor\\staticweb";
        }
        private string RootDirectory {get;}
        public void GetLocalPath(string directory, string requestedFile)
        {
            if(directory == "/" && requestedFile != "")
            {
                PhysicalPath = $"{RootDirectory}\\{requestedFile}";
            }
            else if(requestedFile != "")
            {
                PhysicalPath = $"{RootDirectory}\\{directory}\\{requestedFile}";
            }
            else
            {
                PhysicalPath = $"{RootDirectory}\\{ErrorPage}";
            }
        }

        public string GetTheDefaultFileName(string localDirectory)
        {
            throw new NotImplementedException();
        }

        public void GetFileData(string requestedFile)
        {
            int iTotBytes = 0;
            string response = "";
            FileStream fs = new FileStream(PhysicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader reader = new BinaryReader(fs);

            byte[] bytes = new byte[fs.Length];
            int read;
            while ((read = reader.Read(bytes, 0, bytes.Length)) != 0)
            {
                response = response + Encoding.ASCII.GetString(bytes, 0, read);
                iTotBytes = iTotBytes + read;
            }
            Console.WriteLine($"response : {response}");
            string mimeType = MIMEAssistant.GetMIMEType(requestedFile);
            Response.MimeType = mimeType;
            Response.ResponseLength = iTotBytes;
            Console.WriteLine($"***mimeType*** : {mimeType}");
            Response.StatusCode = " 200 OK";
            Response.ResponseBody = bytes;
            reader.Close();
            fs.Close();

             Dispatcher.GenerateResponse();
            Dispatcher.SendToBrowser(bytes);
            // Response.conn.Send(Encoding.ASCII.GetBytes(response));

        }

    }
}
