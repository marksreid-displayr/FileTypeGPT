namespace ConsoleApp.File;

public class TVShow(FileEntry fileEntry, string name, string? resolution, int season, int? episode) : Video(fileEntry, name, resolution)
{
    public int Season { get; } = season;
    public int? Episode { get; } = episode;

    public override void Move(string destinationPrefix)
    {
        var destination = Path.Combine(Name, $"season {Season}");
        if (Episode is not null)
        {
            destination = Path.Combine(destination, $"episode {Episode}");
        }
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