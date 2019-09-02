using System;
using System.IO;

namespace WebServerRefactor
{
    public class Utility
    {
       public static string GetMimeType(string requestedFile)
        {
            StreamReader sr;
            String Line = "";
            String MimeType = "";
            String FileExt = "";
            String MimeExt = "";
            requestedFile = requestedFile.ToLower();
            int startPos = requestedFile.IndexOf(".");
            FileExt = requestedFile.Substring(startPos);
            Console.WriteLine($"FileExt : {FileExt}");
            try
            {
                sr = new StreamReader("Utility\\MimeType.dat");
                while ((Line = sr.ReadLine()) != null)
                {
                    Line.Trim();
                    if(Line.Length > 0)
                    {
                        startPos = Line.IndexOf(";");
                        Line = Line.ToLower();
                        MimeExt = Line.Substring(0, startPos);
                        MimeType = Line.Substring(startPos + 1);
                        if (FileExt == MimeExt)
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("An Exception Occurred : " + ex.ToString());
            }

            Console.WriteLine($"MimeExt{MimeExt}, MimeType{MimeType}");
            if (MimeExt == FileExt)
                return MimeType;
            return "";
        }
    }
}
