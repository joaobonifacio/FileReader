using FileReaderApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderApp.Role
{
    public class SimpleRoleValidator: IRoleValidator
    {
        private readonly Role _currentRole;

        public SimpleRoleValidator(Role role)
        {
            _currentRole = role;
        }

        public bool CanAccess(string[] allowedRoles)
        {
            return allowedRoles.Any(r => string.Equals(r.Trim(), _currentRole.ToString(), StringComparison.OrdinalIgnoreCase));
        }
    }
}
