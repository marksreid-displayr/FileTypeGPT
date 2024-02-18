using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using ConsoleApp.File;
using Microsoft.Extensions.Options;
using Serilog;

namespace ConsoleApp;
class Program
{
    static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        // Resolve and run the application

        var test = host.Services.GetRequiredService<IFileMover>();

        var consoleApplication = host.Services.GetRequiredService<MyConsoleApplication>();
        await consoleApplication.Run();

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                // Register your services and dependencies here
                services.AddSingleton<MyConsoleApplication>();
                services.AddTransient<IOpenAIService, OpenAIService>();
                services.AddSingleton<IFileClassifier, FileClassifier>();
                services.AddSingleton<IFileCollector, FileCollector>();
                services.AddSingleton<IFileSystem, FileSystem>();
                services.AddSingleton<IFileMoveOperation, FileMoveOperation>();
                services.AddSingleton<IFileMover, FileMover>();
                services.AddSingleton<IFileSerializer>(sp =>
                    ActivatorUtilities.CreateInstance<FileSerializer>(sp, new JsonSerializerOptions
                    {
                        Converters = { new FileTypeConverter() }
                    }));
                services.AddSingleton<IPromptGenerator, PromptGenerator>();

                services.Configure<ChatGPT>(hostContext.Configuration.GetSection(nameof(ChatGPT)));
                services.Configure<OpenAI>(hostContext.Configuration.GetSection(nameof(OpenAI)));
                services.Configure<Files>(hostContext.Configuration.GetSection(nameof(Files)));
                services.AddSingleton<IValidateOptions<Files>, FilesValidator>();
            });
}