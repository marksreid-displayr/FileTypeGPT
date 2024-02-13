using ConsoleApp.File;

namespace ConsoleApp;

public class ChatGPT
{
    public string? ApiKey { get; set; }
    public string[]? Prompt { get; set; }
    public FileInformation? PromptExample { get; set; }
}