using Microsoft.Extensions.Options;

namespace ConsoleApp;

public class FileCollector(IOptions<Files> files) : IFileCollector
{
    public FileEntry[] GetFiles()
    {
        var fileEntries = Directory.GetFiles(files.Value.Source!).Where(entry =>
                !(files.Value.Exclusions?.Any(exclusion => exclusion == entry) ?? false))
            .Select(file => new FileEntry(files.Value.Source!, file));

        var directoryEntries = Directory.GetDirectories(files.Value.Source!).Where(entry =>
                !(files.Value.Exclusions?.Any(exclusion => exclusion == entry) ?? false))
            .Select(dir => new DirectoryEntry(files.Value.Source!, dir, Directory.GetFiles(Path.Combine(files.Value.Source!,dir)).Length));

        return fileEntries.Concat(directoryEntries).ToArray();
    }
}

public record FileEntry(string Name, string Directory);
public record DirectoryEntry(string Name, string Directory, int FileCount) : FileEntry(Name, Directory);