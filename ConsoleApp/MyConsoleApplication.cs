using Microsoft.Extensions.Logging;

namespace ConsoleApp;

public class MyConsoleApplication(IFileClassifier fileClassifier, IFileCollector fileCollector, IFileMover fileMover, ILogger<MyConsoleApplication> logger)
{
    public async Task Run()
    {
        var files = fileCollector.GetFiles();
        foreach (var fileChunk in files.Chunk(10))
        {
            var classifiedFiles = await fileClassifier.ClassifyFilesAsync(fileChunk, () => Console.WriteLine("."));
            if (classifiedFiles is null)
            {
                continue;
            }
            fileMover.MoveFiles(classifiedFiles);
        }
    }
}