using FileReaderApp.Interfaces;
using FileReaderApp.Role;
using System.Text.Json;
using UserRole = FileReaderApp.Role.Role;

namespace FileReaderApp.FileReaders
{
    public class SecuredJsonFileReader : IFileReader
    {
        private readonly IRoleValidator _roleValidator;

        public SecuredJsonFileReader(IRoleValidator roleValidator)
        {
            _roleValidator = roleValidator;
        }

        public string Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            string json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });

            writer.WriteStartObject();

            // PUBLIC
            if (root.TryGetProperty("public", out var publicSection))
            {
                writer.WritePropertyName("public");
                publicSection.WriteTo(writer);
            }

            // SHARD
            if (_roleValidator.IsInRole(UserRole.Admin) || _roleValidator.IsInRole(UserRole.Employee))
            {
                if (root.TryGetProperty("shared", out var sharedSection))
                {
                    writer.WritePropertyName("shared");
                    sharedSection.WriteTo(writer);
                }
            }

            // ADMIN
            if (_roleValidator.IsInRole(UserRole.Admin))
            {
                if (root.TryGetProperty("admin", out var adminSection))
                {
                    writer.WritePropertyName("admin");
                    adminSection.WriteTo(writer);
                }
            }

            writer.WriteEndObject();
            writer.Flush();

            return System.Text.Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
