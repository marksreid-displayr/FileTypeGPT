using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ConsoleApp;
class Program
{
    static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        // Resolve and run the application
        var fileCollector = host.Services.GetRequiredService<IFileCollector>();
        fileCollector.GetFiles();

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                // Register your services and dependencies here
                services.AddSingleton<MyConsoleApplication>();
                services.AddTransient<IOpenAIService, OpenAIService>();
                services.AddSingleton<IFileClassifier, FileClassifier>();
                services.Configure<ChatGPT>(hostContext.Configuration.GetSection(nameof(ChatGPT)));
                services.Configure<OpenAI>(hostContext.Configuration.GetSection(nameof(OpenAI)));
                services.AddSingleton<IValidateOptions<Files>, FilesValidator>();
                services.AddSingleton<IFileCollector, FileCollector>();
                services.Configure<Files>(hostContext.Configuration.GetSection(nameof(Files)));
                services.AddSingleton(new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                services.AddSingleton<IPromptGenerator, PromptGenerator>();
            });
}