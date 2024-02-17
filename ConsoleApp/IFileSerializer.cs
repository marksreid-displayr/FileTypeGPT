using ConsoleApp.File;

namespace ConsoleApp;

public interface IFileSerializer
{
    BaseFile[]? SerializeToStrongType(string answer, IEnumerable<string> fileNames, IReadOnlyDictionary<string, FileEntry> filesByName);
}