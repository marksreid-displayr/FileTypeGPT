namespace ConsoleApp;

public interface IPromptGenerator
{
    List<string> GetPrompt(string[] files);
}