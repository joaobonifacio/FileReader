using FileReaderApp.Interfaces;
using System.Text.Json;

namespace FileReaderApp.FileReaders
{
    public class JsonFileReader : IFileReader
    {
        public string Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            string json = File.ReadAllText(path);

            using var jsonDoc = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
