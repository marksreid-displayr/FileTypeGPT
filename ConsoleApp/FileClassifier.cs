using System.Diagnostics;
using System.IO;
using System.Text.Json;
using ConsoleApp.File;
using Microsoft.Extensions.Options;

namespace ConsoleApp;

public class FileClassifier(IOpenAIService openAIService, IPromptGenerator promptGenerator)
    : IFileClassifier
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new FileTypeConverter() }
    };

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

        var fileInformation = JsonSerializer.Deserialize<FileInformation[]>(answer, _jsonSerializerOptions) ?? [];
        var missingFiles = fileNames.Except(fileInformation.Select(fileInfo => fileInfo.OriginalFilename))
            .Select(missing => new MissingFile(filesByName[missing!])).ToArray();
        return fileInformation?.Select(fi => fi.AsStrongType(filesByName[fi.OriginalFilename!])).Concat(missingFiles).ToArray();
    }
}

public interface IFileMover
{
    void MoveFiles(IEnumerable<BaseFile> fileInformation);
}

public class FileMover(IOptions<Files> fileOptions) : IFileMover
{
    public void MoveFiles(IEnumerable<BaseFile> fileInformation)
    {
        foreach (var file in fileInformation)
        {
            if (file is InvalidFile)
            {
                continue;
            }

            var destinationBase = file switch
            {
                MissingFile => fileOptions.Value.Destination!.Missing,
                TVShow => fileOptions.Value.Destination!.TV,
                Movie => fileOptions.Value.Destination!.Movies,
                Other => fileOptions.Value.Destination!.Other,
                _ => throw new($"File of type {file.GetType()} was unexpected")
            };

            if (file is IFileCanBeMoved fileHasDestination && destinationBase is not null)
            {
                fileHasDestination.Move(destinationBase);
            }
        }
    }
}
