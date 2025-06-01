using FileReaderApp.FileReaders;
using FileReaderApp.Interfaces;
using System;
using System.IO; 

class Program
{
    static void Main()
    {
        //Ask user for file type
        Console.WriteLine("Choose file type:");
        Console.WriteLine("1 - Text file");
        Console.WriteLine("2 - XML file");

        string typeInput = "";
        while (typeInput != "1" && typeInput != "2")
        {
            Console.Write("Enter your choice (1 or 2): ");
            typeInput = Console.ReadLine()?.Trim();
        }

        IFileReader reader;
        string defaultPath;

        if (typeInput == "1")
        {
            reader = new TextFileReader();
            defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "txt", "sample.txt");
        }
        else
        {
            reader = new XmlFileReader();
            defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "xml", "sample.xml"); 
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
