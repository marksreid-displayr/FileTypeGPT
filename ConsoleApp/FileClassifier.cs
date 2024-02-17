using System.Text.Json;
using ConsoleApp.File;

namespace ConsoleApp;

public class FileClassifier(IOpenAIService openAIService, IPromptGenerator promptGenerator, IFileSerializer fileSerializer)
    : IFileClassifier
{
    public async Task<BaseFile[]?> ClassifyFilesAsync(FileEntry[] files, Action? progressCallback = null)
    {
        var filesByName = files.ToDictionary(key => key.Name, value => value);

        string[] fileNames = [..filesByName.Keys];
        var prompt = promptGenerator.GetPrompt(fileNames);
        var answerTask = openAIService.AnswerPromptAsync(prompt);
        while (!answerTask.IsCompleted)
        {
            progressCallback?.Invoke();
            Thread.Sleep(1000);
        }
        var answer = await answerTask;

        return fileSerializer.SerializeToStrongType(answer, fileNames, filesByName);
    }

}

public interface IFileSerializer
{
    BaseFile[]? SerializeToStrongType(string answer, IEnumerable<string> fileNames, IReadOnlyDictionary<string, FileEntry> filesByName);
}

public class FileSerializer : IFileSerializer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new FileTypeConverter() }
    };

    public BaseFile[]? SerializeToStrongType(string answer, IEnumerable<string> fileNames, IReadOnlyDictionary<string, FileEntry> filesByName)
    {
        var fileInformation = JsonSerializer.Deserialize<FileInformation[]>(answer, _jsonSerializerOptions) ?? [];
        var missingFiles = fileNames.Except(fileInformation.Select(fileInfo => fileInfo.OriginalFilename))
            .Select(missing => new MissingFile(filesByName[missing!])).ToArray();
        return fileInformation?.Select(fi => fi.AsStrongType(filesByName[fi.OriginalFilename!])).Concat(missingFiles).ToArray();
    }
}
