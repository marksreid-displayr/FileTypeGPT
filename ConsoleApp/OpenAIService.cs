using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;

namespace ConsoleApp;

public class OpenAIService(IOptions<OpenAI> openAiOptions) : IOpenAIService
{
    private OpenAIClient Client { get; } = new(openAiOptions.Value.Credentials);

    public async Task<string> AnswerPromptAsync(IEnumerable<string> prompt)
    {
        var completionsOptions = new ChatCompletionsOptions("gpt-4"/*"gpt-4-1106-preview"*/, prompt.Select(content => new ChatMessage(ChatRole.User, content)))
        {
            DeploymentName = "gpt-4"//"gpt-4-1106-preview"
        };
        var completions = await Client.GetChatCompletionsAsync(completionsOptions);
        return completions.Value.Choices[0].Message.Content;

    }
}
