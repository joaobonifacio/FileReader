using FileReaderApp.FileReaders;
using FileReaderApp.Encryption;
using FileReaderApp.Interfaces;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Choose file type:");
        Console.WriteLine("1 - Plain Text");
        Console.WriteLine("2 - XML");
        Console.WriteLine("3 - Encrypted Text (reverse-character encryption only)");

        string typeInput = "";
        while (typeInput != "1" && typeInput != "2" && typeInput != "3")
        {
            Console.Write("Enter your choice (1 = TXT, 2 = XML, 3 = Encrypted TXT (reverse-character encryption only)): ");
            typeInput = Console.ReadLine()?.Trim();
        }

        IFileReader reader;
        string defaultPath;

        if (typeInput == "1")
        {
            reader = new TextFileReader();
            defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "txt", "sample.txt");
        }
        else if (typeInput == "2")
        {
            reader = new XmlFileReader();
            defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "xml", "sample.xml");
        }
        else // Encrypted Text
        {
            Console.WriteLine("Only reverse-character encryption is supported (e.g. 'olleH' decrypts to 'Hello').");

            var encryption = new ReverseEncryption();
            reader = new EncryptedTextFileReader(encryption);
            defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "txt", "encrypted.txt");
        }

        Console.Write($"Enter the path to the file, or press ENTER to use default ({defaultPath}): ");
        string inputPath = Console.ReadLine();

        string filePath = string.IsNullOrWhiteSpace(inputPath) ? defaultPath : inputPath;

        try
        {
            string content = reader.Read(filePath);
            Console.WriteLine("\n--- File Content ---");
            Console.WriteLine(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine("\n Could not read file:");
            Console.WriteLine(ex.Message);
        }
    }
}
