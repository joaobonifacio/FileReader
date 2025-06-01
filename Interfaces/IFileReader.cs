using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderApp.Interfaces
{
    public interface IFileReader
    {
        string Read(string path);
    }
}
