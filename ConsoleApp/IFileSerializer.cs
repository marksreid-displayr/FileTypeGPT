using ConsoleApp.File;

namespace ConsoleApp;

public interface IFileSerializer
{
    BaseFile[]? SerializeToStrongType(string answer, IEnumerable<FileEntry> files);
}