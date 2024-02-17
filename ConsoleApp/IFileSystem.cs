namespace ConsoleApp;

public interface IFileSystem
{
    string[] GetFiles(string path);
    string[] GetDirectories(string path);
    int GetFileCount(string path);
    void CreateDirectory(string destinationFolder);
    string[] GetFileSystemEntries(string source);
    void Move(string source, string destination);
    void Delete(string path);
    bool Exists(string directory);
}