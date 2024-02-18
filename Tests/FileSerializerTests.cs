using System.Text.Json;
using ConsoleApp;
using ConsoleApp.File;

namespace Tests
{
    public class FileSerializerTests
    {
        [Fact]
        public void CanDeserialize()
        {
            var sut = new FileSerializer(new()
            {
                Converters = { new FileTypeConverter() }
            });
            const string answer = """
                                  [
                                    {
                                      "Type": "tvshow",
                                      "Name": "Rick.and.Morty",
                                      "Season": "07",
                                      "Episode": "09",
                                      "Resolution": "720p",
                                      "OriginalFilename": "Rick.and.Morty.S07E09.720p.WEB.H264-NHTFS[TGx]"
                                    }
                                  ]
                                  """;
            var strongTypes = sut.SerializeToStrongType(answer, [new DirectoryEntry("Rick.and.Morty.S07E09.720p.WEB.H264-NHTFS[TGx]","", 5)]);
            if (strongTypes != null)
            {
                Assert.Single(strongTypes);
                Assert.IsType<TVShow>(strongTypes[0]);
            }
        }
    }
}