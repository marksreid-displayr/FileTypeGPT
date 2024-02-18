using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ConsoleApp;

public class PromptGenerator(IOptions<ChatGPT> chatGPT) : IPromptGenerator
{
    public List<string> GetPrompt(string[] files)
    {
        var prompt = (chatGPT.Value.Prompt ?? Enumerable.Empty<string>()).ToList();
        prompt.AddRange(chatGPT.Value.PromptExample ?? Enumerable.Empty<string>());
        prompt.Add("");
        prompt.AddRange(files);
        return prompt;
    }
}