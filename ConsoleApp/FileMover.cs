using ConsoleApp.File;
using Microsoft.Extensions.Options;

namespace ConsoleApp;

public class FileMover(IOptions<Files> fileOptions, IFileMoveOperation fileMoveOperation) : IFileMover
{
    public void MoveFiles(IEnumerable<BaseFile> fileInformation)
    {
        foreach (var file in fileInformation)
        {
            if (file is InvalidFile)
            {
                continue;
            }

            var destinationBase = file switch
            {
                MissingFile => fileOptions.Value.Destination!.Missing,
                TVShow => fileOptions.Value.Destination!.TV,
                Movie => fileOptions.Value.Destination!.Movies,
                Other => fileOptions.Value.Destination!.Other,
                _ => throw new($"File of type {file.GetType()} was unexpected")
            };

            if (file is IFileCanBeMoved fileHasDestination && destinationBase is not null)
            {
                fileHasDestination.Move(destinationBase, fileMoveOperation);
            }
        }
    }
}