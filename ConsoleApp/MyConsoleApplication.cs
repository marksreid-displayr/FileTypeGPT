using System.Text;
using System.Text.Json;

namespace ConsoleApp;

public class MyConsoleApplication(IFileClassifier fileClassifier, JsonSerializerOptions jsonSerializerOptions)
{
    public async Task Run(string[] files)
    {
        //var fileInformation = await fileClassifier.ClassifyFilesAsync(files, () => Console.Write("."));
        //Console.WriteLine();
        //await using var answerFile = File.Create(@"c:\temp\answers.json");
        //await JsonSerializer.SerializeAsync(answerFile, fileInformation, jsonSerializerOptions);
    }
}