using FileReaderApp.FileReaders;
using FileReaderApp.Encryption;
using FileReaderApp.Interfaces;
using FileReaderApp.Role;
using UserRole = FileReaderApp.Role.Role;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Choose file type:");
        Console.WriteLine("1 - Plain Text");
        Console.WriteLine("2 - XML");
        Console.WriteLine("3 - Encrypted Text (reverse only)");
        Console.WriteLine("4 - XML with Role-Based Access");
        Console.WriteLine("5 - Encrypted XML (reverse only)"); 

        string typeInput = "";
        while (!new[] { "1", "2", "3", "4", "5" }.Contains(typeInput)) 
        {
            Console.Write("Enter your choice (1 to 5): "); 
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
        else if (typeInput == "3")
        {
            Console.WriteLine("Reverse character decryption only.");
            var encryption = new ReverseEncryption();
            reader = new EncryptedTextFileReader(encryption);
            defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "txt", "encrypted.txt");
        }
        else if (typeInput == "5") 
        {
            Console.WriteLine("Reverse character decryption only (for XML).");
            var encryption = new ReverseEncryption();
            reader = new EncryptedXmlFileReader(encryption); 
            defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "xml", "encrypted.xml");
        }
        else // typeInput == "4"
        {
            Console.WriteLine("Select your role:");
            Console.WriteLine("1 - Admin");
            Console.WriteLine("2 - Employee");

            string roleInput = "";
            while (roleInput != "1" && roleInput != "2")
            {
                Console.Write("Enter role number: ");
                roleInput = Console.ReadLine()?.Trim();
            }

            UserRole userRole = roleInput == "1" ? UserRole.Admin : UserRole.Employee;
            var validator = new SimpleRoleValidator(userRole);
            reader = new SecuredXmlFileReader(validator);

            defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "xml", "role.xml");
        }

        Console.Write($"Enter path to file or press ENTER to use default ({defaultPath}): ");
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
