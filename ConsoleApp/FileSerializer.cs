using System.Text.Json;
using ConsoleApp.File;

namespace ConsoleApp;

public class FileSerializer(JsonSerializerOptions jsonSerializerOptions) : IFileSerializer
{
    public BaseFile[]? SerializeToStrongType(string answer, IEnumerable<FileEntry> files)
    {
        var fileEntries = files as FileEntry[] ?? files.ToArray();

        var fileNames = fileEntries.Select(fileEntry => fileEntry.Name).ToArray();

        var filesByName = fileEntries.ToDictionary(key => key.Name, value => value);

        var fileInformation = JsonSerializer.Deserialize<FileInformation[]>(answer, jsonSerializerOptions) ?? [];

        var originalFileNames = fileInformation.Where(fileInfo => fileInfo.OriginalFilename is not null)
            .Select(fileInfo => fileInfo.OriginalFilename!).ToArray();

        var missingFiles = fileNames.Except(originalFileNames)
            .Select(missing => new MissingFile(filesByName[missing])).ToArray();

        return fileInformation.Where(fi => filesByName.ContainsKey(fi.OriginalFilename!))
            .Select(fi => fi.AsStrongType(filesByName[fi.OriginalFilename!])).Concat(missingFiles).ToArray();
    }
}