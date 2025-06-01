using FileReaderApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderApp.FileReaders
{
    public class EncryptedTextFileReader : IFileReader
    {
        private readonly IEncryptionStrategy _encryption;

        public EncryptedTextFileReader(IEncryptionStrategy encryption)
        {
            _encryption = encryption;
        }

        public string Read(string path)
        {
            string encryptedContent = File.ReadAllText(path);
            return _encryption.Decrypt(encryptedContent);
        }
    }
}
