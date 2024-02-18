namespace ConsoleApp.File;

public class NamedFile(FileEntry fileEntry) : BaseFile
{
    public FileEntry FileEntry { get; } = fileEntry;
}