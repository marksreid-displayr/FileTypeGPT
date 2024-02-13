using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp;

public class FileTypeConverter : JsonConverter<FileType>
{
    public override FileType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var enumValue = reader.GetString()?.ToLower();
        return enumValue switch
        {
            "movie" => FileType.Movie,
            "tvshow" => FileType.TVShow,
            "other" => FileType.Other,
            null => throw new ArgumentException("Null status value"),
            _ => throw new ArgumentException($"Invalid status value {enumValue}")
        };
    }

    public override void Write(Utf8JsonWriter writer, FileType value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}