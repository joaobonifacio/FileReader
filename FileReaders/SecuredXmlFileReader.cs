using FileReaderApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileReaderApp.FileReaders
{
    public class SecuredXmlFileReader: IFileReader
    {
        private readonly IRoleValidator _roleValidator;

        public SecuredXmlFileReader(IRoleValidator roleValidator)
        {
            _roleValidator = roleValidator;
        }

        public string Read(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);

            var sb = new StringBuilder();
            sb.AppendLine("<filteredReport>");

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Attributes?["role"] == null)
                {
                    sb.AppendLine(node.OuterXml);
                }
                else
                {
                    var roles = node.Attributes["role"].Value.Split(",");
                    if (_roleValidator.CanAccess(roles))
                    {
                        sb.AppendLine(node.OuterXml);
                    }
                }
            }

            sb.AppendLine("</filteredReport>");
            return sb.ToString();
        }
    }
}
