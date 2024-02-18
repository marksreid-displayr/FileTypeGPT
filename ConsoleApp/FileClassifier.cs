using ConsoleApp.File;
using Microsoft.Extensions.Logging;

namespace ConsoleApp;

public class FileClassifier(IOpenAIService openAIService, IPromptGenerator promptGenerator, IFileSerializer fileSerializer, ILogger<FileClassifier> logger)
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
        logger.LogInformation(answer);

        return fileSerializer.SerializeToStrongType(answer, files);
    }

}