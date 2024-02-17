namespace ConsoleApp;

public record DirectoryEntry(string Name, string Directory, int FileCount) : FileEntry(Name, Directory);