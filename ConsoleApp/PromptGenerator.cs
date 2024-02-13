using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ConsoleApp;

public class PromptGenerator(IOptions<ChatGPT> chatGPT, JsonSerializerOptions jsonSerializerOptions) : IPromptGenerator
{
    private static readonly char[] PromptSeparator = ['\n', '\r'];

    public List<string> GetPrompt(string[] files)
    {
        var prompt = (chatGPT.Value.Prompt ?? Enumerable.Empty<string>()).ToList();
        prompt.AddRange(Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(chatGPT.Value.PromptExample, jsonSerializerOptions)).Split(PromptSeparator, StringSplitOptions.RemoveEmptyEntries));
        prompt.Add("");
        prompt.AddRange(files);
        return prompt;
    }
}