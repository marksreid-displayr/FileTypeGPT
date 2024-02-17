namespace ConsoleApp.File;

public interface IFileCanBeMoved
{
    void Move(string destinationPrefix, IFileMoveOperation fileMover);
}