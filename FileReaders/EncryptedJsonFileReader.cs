using FileReaderApp.Interfaces;
using System.Text.Json;

namespace FileReaderApp.FileReaders
{
    public class EncryptedJsonFileReader : IFileReader
    {
        private readonly IEncryptionStrategy _encryption;

        public EncryptedJsonFileReader(IEncryptionStrategy encryption)
        {
            _encryption = encryption;
        }

        public string Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            string encryptedContent = File.ReadAllText(path);
            string decryptedContent = _encryption.Decrypt(encryptedContent);

            try
            {
                using var doc = JsonDocument.Parse(decryptedContent);
                return JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (JsonException ex)
            {
                return $"Decryption worked, but JSON is invalid:\n{ex.Message}\n\nDecrypted raw content:\n{decryptedContent}";
            }
        }
    }
}
