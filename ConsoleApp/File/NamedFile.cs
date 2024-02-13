namespace ConsoleApp.File;

public class NamedFile(FileEntry fileEntry) : BaseFile
{
    public FileEntry FileEntry { get; } = fileEntry;

    protected void MoveContentsOfFolderAndDeleteSourceFolder(string destinationPrefix, string destination)
    {
        var destinationFolder = Path.Join(destinationPrefix, destination);
        Directory.CreateDirectory(destinationFolder);
        var source = Path.Join(FileEntry.Directory, FileEntry.Name);
        foreach (var file in Directory.GetFileSystemEntries(source))
        {
            Directory.Move(Path.Join(source,file), destinationFolder);
        }
        Directory.Delete(source);
    }

    protected void MoveOnly(string destinationPrefix, string destination)
    {
        var destinationFolder = Path.Join(destinationPrefix, destination);
        Directory.CreateDirectory(destinationFolder);
        var source = Path.Join(FileEntry.Directory, FileEntry.Name);
        Directory.Move(source, destination);
    }

    protected void RemoveEmptyDirectory()
    {
        var source = Path.Join(FileEntry.Directory, FileEntry.Name);
        Directory.Delete(source);
    }
}