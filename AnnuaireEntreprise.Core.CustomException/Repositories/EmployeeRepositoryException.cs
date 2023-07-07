using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnuaireEntreprise.Core.CustomException.Repositories
{
    public class EmployeeRepositoryException : Exception
    {
        public EmployeeRepositoryException()
        {
        }

        public EmployeeRepositoryException(string? message) : base(message)
        {
        }
    }
}
