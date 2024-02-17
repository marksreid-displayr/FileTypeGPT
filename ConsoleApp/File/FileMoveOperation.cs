namespace ConsoleApp.File;

public class FileMoveOperation(IFileSystem fileSystem) : IFileMoveOperation
{
    public void MoveContentsOfFolderAndDeleteSourceFolder(FileEntry fileEntry, string destinationPrefix, string destination)
    {
        var destinationFolder = Path.Join(destinationPrefix, destination);
        fileSystem.CreateDirectory(destinationFolder);
        var source = Path.Join(fileEntry.Directory, fileEntry.Name);
        foreach (var file in fileSystem.GetFileSystemEntries(source))
        {
            fileSystem.Move(Path.Join(source,file), destinationFolder);
        }
        fileSystem.Delete(source);
    }

    public void MoveOnly(FileEntry fileEntry,string destinationPrefix, string destination)
    {
        var destinationFolder = Path.Join(destinationPrefix, destination);
        fileSystem.CreateDirectory(destinationFolder);
        var source = Path.Join(fileEntry.Directory, fileEntry.Name);
        fileSystem.Move(source, destination);
    }

    public void RemoveEmptyDirectory(FileEntry fileEntry)
    {
        var source = Path.Join(fileEntry.Directory, fileEntry.Name);
        fileSystem.Delete(source);
    }

}