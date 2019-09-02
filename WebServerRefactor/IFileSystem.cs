namespace WebServerRefactor
{
    public interface IFileSystem
    {
        void GetLocalPath(string directory, string requestedFile);
        string GetTheDefaultFileName(string localDirectory);
    }
}
