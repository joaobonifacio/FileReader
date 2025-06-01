using FileReaderApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileReaderApp.FileReaders
{
    public class XmlFileReader : IFileReader
    {
        public string Read(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            return doc.OuterXml;
        }
    }
}
