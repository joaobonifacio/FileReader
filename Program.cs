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
        while (true)
        {
            Console.WriteLine("\n Choose file type:");
            Console.WriteLine("1 - Text");
            Console.WriteLine("2 - XML");
            Console.WriteLine("3 - JSON");
            Console.WriteLine("Q - Quit");

            Console.Write("Your choice: ");
            var fileType = Console.ReadLine()?.Trim().ToUpper();

            if (fileType == "Q") break;

            if (!new[] { "1", "2", "3" }.Contains(fileType))
            {
                Console.WriteLine(" Invalid option.");
                continue;
            }

            Console.Write(" Use encryption? (y/n): ");
            var encryptInput = Console.ReadLine()?.Trim().ToLower();
            bool useEncryption = encryptInput == "y";

            bool useRoles = false;

            if (useEncryption)
            {
                Console.WriteLine("Encryption selected means Role-based access is disabled.");
            }
            else
            {
                Console.Write(" Use role-based access? (y/n): ");
                var roleInput = Console.ReadLine()?.Trim().ToLower();
                useRoles = roleInput == "y";
            }

            if (useEncryption && useRoles)
            {
                Console.WriteLine("\n Encryption and Role-Based Access cannot be combined.");
                continue;
            }

            UserRole role = UserRole.Employee;
            if (useRoles)
            {
                Console.WriteLine("Select role: 1 - Admin | 2 - Employee");
                string roleNum = "";
                while (roleNum != "1" && roleNum != "2")
                {
                    Console.Write("Enter role number: ");
                    roleNum = Console.ReadLine()?.Trim();
                }
                role = roleNum == "1" ? UserRole.Admin : UserRole.Employee;
            }

            IFileReader reader;
            string defaultPath = "";

            if (fileType == "1") // Text
            {
                if (useEncryption)
                {
                    reader = new EncryptedTextFileReader(new ReverseEncryption());
                    defaultPath = GetPath("txt", "encrypted.txt");
                }
                else if (useRoles)
                {
                    reader = new SecuredTextFileReader(new SimpleRoleValidator(role));
                    defaultPath = GetPath("txt", "role.txt");
                }
                else
                {
                    reader = new TextFileReader();
                    defaultPath = GetPath("txt", "sample.txt");
                }
            }
            else if (fileType == "2") // XML
            {
                if (useEncryption)
                {
                    reader = new EncryptedXmlFileReader(new ReverseEncryption());
                    defaultPath = GetPath("xml", "encrypted.xml");
                }
                else if (useRoles)
                {
                    reader = new SecuredXmlFileReader(new SimpleRoleValidator(role));
                    defaultPath = GetPath("xml", "role.xml");
                }
                else
                {
                    reader = new XmlFileReader();
                    defaultPath = GetPath("xml", "sample.xml");
                }
            }
            else // JSON
            {
                if (useEncryption)
                {
                    reader = new EncryptedJsonFileReader(new ReverseEncryption());
                    defaultPath = GetPath("json", "encrypted.json");
                }
                else if (useRoles)
                {
                    reader = new SecuredJsonFileReader(new SimpleRoleValidator(role));
                    defaultPath = GetPath("json", "role.json");
                }
                else
                {
                    reader = new JsonFileReader();
                    defaultPath = GetPath("json", "sample.json");
                }
            }

            Console.Write($" Enter path to file or press ENTER to use default ({defaultPath}): ");
            string inputPath = Console.ReadLine();
            string filePath = string.IsNullOrWhiteSpace(inputPath) ? defaultPath : inputPath;

            try
            {
                Console.WriteLine("\n --- File Content ---");
                Console.WriteLine(reader.Read(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Could not read file: {ex.Message}");
            }

            Console.WriteLine("\n Read another file? (y/n): ");
            var again = Console.ReadLine()?.Trim().ToLower();
            if (again != "y") break;
        }

        Console.WriteLine(" Goodbye!");
    }

    static string GetPath(string folder, string filename)
    {
        return Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", folder, filename);
    }
}
