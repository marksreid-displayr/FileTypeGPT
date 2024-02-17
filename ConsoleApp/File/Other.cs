namespace ConsoleApp.File;

public class Other(FileEntry fileEntry) : NamedFile(fileEntry), IFileCanBeMoved
{
    public virtual void Move(string destinationPrefix, IFileMoveOperation fileMover)
    {
        fileMover.MoveOnly(FileEntry, destinationPrefix, "");
    }
}