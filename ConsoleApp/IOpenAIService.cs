namespace ConsoleApp;

public interface IOpenAIService
{
    Task<string> AnswerPromptAsync(IEnumerable<string> prompt);
}