namespace ConsoleApp.File;

public class MissingFile(FileEntry originalFilename) : NamedFile(originalFilename), IFileCanBeMoved
{
    public virtual void Move(string destinationPrefix, IFileMoveOperation fileMover)
    {
        fileMover.MoveOnly(FileEntry, destinationPrefix, "");
    }
}