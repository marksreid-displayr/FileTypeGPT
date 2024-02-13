using ConsoleApp.File;

namespace ConsoleApp;

public interface IFileMover
{
    void MoveFiles(IEnumerable<BaseFile> fileInformation);
}