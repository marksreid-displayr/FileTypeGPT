using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;

namespace ConsoleApp;

public class FilesValidator(IFileSystem fileSystem) : IValidateOptions<Files>
{
    public ValidateOptionsResult Validate(string? name, Files options)
    {
        return CheckDirectory(options.Source) ??
               CheckDirectory(options.Destination?.Movies) ??
               CheckDirectory(options.Destination?.Other) ??
               CheckDirectory(options.Destination?.TV) ??
               CheckDirectory(options.Destination?.Missing) ??
               ValidateOptionsResult.Success;
    }

    private ValidateOptionsResult? CheckDirectory(string? directory, [CallerArgumentExpression(nameof(directory))]string? directoryName = null) 
    {
        if (directory is not null && fileSystem.Exists(directory)) return null;
        var parameterNameWithoutLeadingOption = string.Join('.',directoryName?.Split('.').Skip(1) ?? []).Replace("?","");
        return ValidateOptionsResult.Fail($"{parameterNameWithoutLeadingOption} must refer to a directory");

    }
}