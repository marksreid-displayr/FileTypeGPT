using System.Text.Json;
using ConsoleApp.File;

namespace ConsoleApp;

public class FileSerializer : IFileSerializer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new FileTypeConverter() }
    };

    public BaseFile[]? SerializeToStrongType(string answer, IEnumerable<string> fileNames, IReadOnlyDictionary<string, FileEntry> filesByName)
    {
        var fileInformation = JsonSerializer.Deserialize<FileInformation[]>(answer, _jsonSerializerOptions) ?? [];
        var missingFiles = fileNames.Except(fileInformation.Select(fileInfo => fileInfo.OriginalFilename))
            .Select(missing => new MissingFile(filesByName[missing!])).ToArray();
        return fileInformation?.Select(fi => fi.AsStrongType(filesByName[fi.OriginalFilename!])).Concat(missingFiles).ToArray();
    }
}