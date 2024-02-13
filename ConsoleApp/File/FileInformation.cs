namespace ConsoleApp.File;

public class FileInformation
{
    public string? OriginalFilename { get; set; }
    public FileType? Type { get; set; }
    public string? Name { get; set; }
    public int? Year { get; set; }
    public int? Season { get; set; }
    public int? Episode { get; set; }
    public string? Resolution { get; set; }

    public BaseFile AsStrongType(FileEntry fileEntry) =>
        Type switch
        {
            FileType.Movie when !string.IsNullOrWhiteSpace(OriginalFilename) && !string.IsNullOrWhiteSpace(Name)
                => new Movie(fileEntry, Name, Resolution, Year),
            FileType.TVShow when Season is not null && !string.IsNullOrWhiteSpace(OriginalFilename) && !string.IsNullOrWhiteSpace(Name)
                => new TVShow(fileEntry, Name, Resolution, Season.Value, Episode),
            FileType.Other when !string.IsNullOrWhiteSpace(OriginalFilename)
                => new Other(fileEntry),
            _ => new InvalidFile()
        };
}