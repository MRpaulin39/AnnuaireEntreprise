using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnuaireEntreprise.Core.CustomException.Repositories
{
    public class ServiceRepositoryException : Exception
    {
        public ServiceRepositoryException()
        {
        }

        public ServiceRepositoryException(string? message) : base(message)
        {
        }
    }
}
