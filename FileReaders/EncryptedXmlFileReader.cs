using FileReaderApp.Encryption;
using FileReaderApp.Interfaces;
using System.Text;
using System.Xml;

namespace FileReaderApp.FileReaders
{
    public class EncryptedXmlFileReader : IFileReader
    {
        private readonly IEncryptionStrategy _encryption;

        public EncryptedXmlFileReader(IEncryptionStrategy encryption)
        {
            _encryption = encryption;
        }

        public string Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Encrypted XML file not found.");

            var doc = new XmlDocument();
            doc.Load(path);

            var sb = new StringBuilder();
            sb.AppendLine("Decrypted Encrypted XML Content:");

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    string encryptedValue = node.InnerText.Trim();
                    string decryptedValue = _encryption.Decrypt(encryptedValue);
                    sb.AppendLine($"- {decryptedValue}");
                }
            }

            return sb.ToString();
        }
    }
}
