using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnuaireEntreprise.Core.Infrastructure.DataLayers
{
    public class EmployeeDataLayer : IEmployeeDataLayers
    {
        public void AddOneEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteOneEmployee(int idEmployee)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployees(int page = 1)
        {
            throw new NotImplementedException();
        }

        public Employee GetOneEmployeeById(int idEmployee)
        {
            throw new NotImplementedException();
        }

        public void UpdateOneEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
