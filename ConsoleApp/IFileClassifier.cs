using ConsoleApp.File;

namespace ConsoleApp;

public interface IFileClassifier
{
    Task<BaseFile[]?> ClassifyFilesAsync(FileEntry[] files, Action? progressCallback = null);
}