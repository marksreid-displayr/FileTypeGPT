namespace ConsoleApp.File;

public interface IFileMoveOperation
{
    void MoveContentsOfFolderAndDeleteSourceFolder(FileEntry fileEntry, string destinationPrefix, string destination);
    void MoveOnly(FileEntry fileEntry,string destinationPrefix, string destination);
    void RemoveEmptyDirectory(FileEntry fileEntry);
}