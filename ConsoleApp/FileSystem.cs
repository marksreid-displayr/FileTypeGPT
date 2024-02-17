namespace ConsoleApp;

public class FileSystem : IFileSystem
{
    public string[] GetFiles(string path) => Directory.GetFiles(path);
    public string[] GetDirectories(string path) => Directory.GetDirectories(path);
    public int GetFileCount(string path) => GetFiles(path).Length;
    public void CreateDirectory(string destinationFolder) => Directory.CreateDirectory(destinationFolder);
    public string[] GetFileSystemEntries(string source) => Directory.GetFileSystemEntries(source);
    public void Move(string source, string destination) => Directory.Move(source, destination);
    public void Delete(string path) => Directory.Delete(path);
    public bool Exists(string directory) => Directory.Exists(directory);
}