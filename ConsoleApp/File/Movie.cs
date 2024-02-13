namespace ConsoleApp.File;

public class Movie(FileEntry fileEntry, string name, string? resolution, int? year) : Video(fileEntry, name, resolution)
{
    public int? Year { get; } = year;
    public override void Move(string destinationPrefix)
    {
        var destination = Year is null ? Name : $"{Name} - {Year}";

        switch (FileEntry)
        {
            case DirectoryEntry { FileCount : 0 }:
                RemoveEmptyDirectory();
                break;
            case DirectoryEntry { FileCount: >= 1 }:
                MoveContentsOfFolderAndDeleteSourceFolder(destinationPrefix, destination);
                break;
            default:
                MoveOnly(destinationPrefix, destination);
                break;
        }
    }
}