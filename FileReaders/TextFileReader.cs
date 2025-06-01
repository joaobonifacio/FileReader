using FileReaderApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderApp.FileReaders
{
    public class TextFileReader: IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
