using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderApp.Interfaces
{
    public interface IRoleValidator
    {
        bool CanAccess(string[] allowedRoles);
    }
}
