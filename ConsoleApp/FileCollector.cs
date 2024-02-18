using Microsoft.Extensions.Options;

namespace ConsoleApp;

public class FileCollector(IOptions<Files> files, IFileSystem fileSystem) : IFileCollector
{
    public FileEntry[] GetFiles()
    {
        var fileEntries = fileSystem.GetFiles(files.Value.Source!).Where(entry =>
                !(files.Value.Exclusions?.Any(exclusion => exclusion == entry) ?? false))
            .Select(file => new FileEntry(files.Value.Source!, file));

        var directoryEntries = fileSystem.GetDirectories(files.Value.Source!).Where(entry =>
                !(files.Value.Exclusions?.Any(exclusion => exclusion == entry) ?? false))
            .Select(dir => new DirectoryEntry(dir,files.Value.Source!, fileSystem.GetFileCount(Path.Combine(files.Value.Source!,dir))));

        return fileEntries.Concat(directoryEntries).ToArray();
    }
}