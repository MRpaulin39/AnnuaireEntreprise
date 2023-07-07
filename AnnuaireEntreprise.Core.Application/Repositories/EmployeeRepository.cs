using AnnuaireEntreprise.Core.CustomException.Repositories;
using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Core.Models;

namespace AnnuaireEntreprise.Core.Application.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Propriétés
        private readonly IEmployeeDataLayer _employeeDataLayer;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISiteRepository _siteRepository;
        #endregion

        #region Constructeur
        public EmployeeRepository(IEmployeeDataLayer employeeDataLayers, IServiceRepository serviceRepository, ISiteRepository siteRepository)
        {
            _employeeDataLayer = employeeDataLayers;
            _serviceRepository = serviceRepository;
            _siteRepository = siteRepository;
        }
        #endregion

        #region Méthodes publiques
        #region Create (Ajout)
        public void AddOneEmployee(Employee employee)
        {
            CheckDataIntegrity(employee, true);

            employee.Service = _serviceRepository.GetOneServiceById(employee.Service.Id);
            employee.Site = _siteRepository.GetOneSiteById(employee.Site.Id);

            _employeeDataLayer.AddOneEmployee(employee);
        }
        #endregion        

        #region Read (Lecture)
        public List<Employee> GetAllEmployees(int page = 1)
        {
            return _employeeDataLayer.GetAllEmployees(page);
        }

        public Employee GetOneEmployeeById(int idEmployee)
        {
            return _employeeDataLayer.GetOneEmployeeById(idEmployee);
        }

        public List<Employee> GetAllEmployeesFiltered(string name, string service, string site)
        {
            return _employeeDataLayer.GetAllEmployeesFiltered(name, service, site);
        }
        #endregion

        #region Update (Mise à jour)
        public void UpdateOneEmployee(Employee employee)
        {
            CheckDataIntegrity(employee, false);

            employee.Service = _serviceRepository.GetOneServiceById(employee.Service.Id);
            employee.Site = _siteRepository.GetOneSiteById(employee.Site.Id);

            _employeeDataLayer.UpdateOneEmployee(employee);
        }
        #endregion

        #region Delete (Suppression)
        public void DeleteOneEmployee(int idEmployee)
        {
            _employeeDataLayer.DeleteOneEmployee(idEmployee);
        }
        #endregion

        #endregion

        #region Méthodes privées
        private void CheckDataIntegrity(Employee employee, bool isNewEmployee) { 
            if (employee == null) { throw new EmployeeRepositoryException("Veuillez définir un salarié à modifier"); }
            if (employee.FirstName == null || employee.FirstName == string.Empty) { throw new EmployeeRepositoryException("Veuillez saisir un prénom"); }
            if (employee.LastName == null || employee.LastName == string.Empty) { throw new EmployeeRepositoryException("Veuillez saisir un nom de famille"); }
            if (employee.Phone == null || employee.Phone == string.Empty) { throw new EmployeeRepositoryException("Veuillez saisir un numéro de téléphone"); }
            if (employee.MobilePhone == null || employee.MobilePhone == string.Empty) { throw new EmployeeRepositoryException("Veuillez saisir un numéro de téléphone mobile"); }
            //Todo : Vérifier que l'adresse email est valide
            if (employee.Mail == null || employee.Mail == string.Empty) { throw new EmployeeRepositoryException("Veuillez saisir une adresse email"); }

            if (employee.Service == null) { throw new EmployeeRepositoryException("Veuillez définir un service"); }
            if (employee.Service.Id == null || employee.Service.Id <= 0) { throw new EmployeeRepositoryException("Veuillez définir un service"); }

            if (employee.Site == null) { throw new EmployeeRepositoryException("Veuillez définir un lieu de travail"); }
            if (employee.Site.Id == null || employee.Site.Id <= 0) { throw new EmployeeRepositoryException("Veuillez définir un lieu de travail"); }

            //Permet si c'est un nouvelle employé de ne pas vérifier l'ID qui n'est pas utile
            if (!isNewEmployee)
            {
                if (employee.Id <= 0) { throw new EmployeeRepositoryException("L'Id du salarié n'est pas défini"); }
            }
        }

        
        #endregion

    }
}
