namespace ConsoleApp.File;

public class Movie(FileEntry fileEntry, string name, string? resolution, int? year) : Video(fileEntry, name, resolution)
{
    public int? Year { get; } = year;
    public override void Move(string destinationPrefix, IFileMoveOperation fileMover)
    {
        var destination = Year is null ? Name : $"{Name} - {Year}";

        switch (FileEntry)
        {
            case DirectoryEntry { FileCount : 0 }:
                fileMover.RemoveEmptyDirectory(FileEntry);
                break;
            case DirectoryEntry { FileCount: >= 1 }:
                fileMover.MoveContentsOfFolderAndDeleteSourceFolder(FileEntry, destinationPrefix, destination);
                break;
            default:
                fileMover.MoveOnly(FileEntry, destinationPrefix, destination);
                break;
        }
    }
}