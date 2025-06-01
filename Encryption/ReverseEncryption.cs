using FileReaderApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderApp.Encryption
{
    public class ReverseEncryption: IEncryptionStrategy
    {
        public string Decrypt(string encrypted)
        {
            return new string(encrypted.Reverse().ToArray());
        }
    }
}
