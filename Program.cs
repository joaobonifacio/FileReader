using FileReaderApp.FileReaders;
using FileReaderApp.Interfaces;

class Program
{
    static void Main()
    {
        string defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "txt", "sample.txt");

        Console.Write($"Enter the path to the text file, or press ENTER to use default ({defaultPath}): ");
        string inputPath = Console.ReadLine();

        string filePath = string.IsNullOrWhiteSpace(inputPath) ? defaultPath : inputPath;

        IFileReader reader = new TextFileReader();

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
