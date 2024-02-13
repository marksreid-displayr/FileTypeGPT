namespace ConsoleApp.File;

public class Video(FileEntry fileEntry, string name, string? resolution) : Other(fileEntry)
{
    public string Name { get; } = name;
    public string? Resolution { get; } = resolution;
}