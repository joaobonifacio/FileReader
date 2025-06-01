using FileReaderApp.Interfaces;
using FileReaderApp.Role;
using System.Text;
using UserRole = FileReaderApp.Role.Role;

namespace FileReaderApp.FileReaders
{
    public class SecuredTextFileReader : IFileReader
    {
        private readonly IRoleValidator _roleValidator;

        public SecuredTextFileReader(IRoleValidator roleValidator)
        {
            _roleValidator = roleValidator;
        }

        public string Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            string[] lines = File.ReadAllLines(path);
            StringBuilder output = new StringBuilder();
            string? currentSection = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = line.Trim('[', ']').ToUpperInvariant();
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (currentSection == "PUBLIC" ||
                    (currentSection == "ADMIN" && _roleValidator.IsInRole(UserRole.Admin)) ||
                    (currentSection == "SHARED" && (_roleValidator.IsInRole(UserRole.Admin) || _roleValidator.IsInRole(UserRole.Employee))))
                                {
                                    output.AppendLine(line);
                                }

            }

            return output.ToString();
        }
    }
}
