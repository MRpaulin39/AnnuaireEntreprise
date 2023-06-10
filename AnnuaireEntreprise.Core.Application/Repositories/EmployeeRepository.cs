using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Core.Models;

namespace AnnuaireEntreprise.Core.Application.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Propriétés
        private readonly IEmployeeDataLayer _employeeDataLayer;
        #endregion

        #region Constructeur
        public EmployeeRepository(IEmployeeDataLayer employeeDataLayers)
        {
            _employeeDataLayer = employeeDataLayers;
        }
        #endregion

        #region Méthodes publiques
        #region Create (Ajout)
        public void AddOneEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
        #endregion        

        #region Read (Lecture)
        public List<Employee> GetAllEmployees(int page = 1)
        {
            return _employeeDataLayer.GetAllEmployees(page);
        }

        public Employee GetOneEmployeeById(int idEmployee)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update (Mise à jour)
        public void UpdateOneEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete (Suppression)
        public void DeleteOneEmployee(int idEmployee)
        {

            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region Méthodes privées

        #endregion

    }
}
