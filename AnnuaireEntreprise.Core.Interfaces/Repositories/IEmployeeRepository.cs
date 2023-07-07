using AnnuaireEntreprise.Core.Models;

namespace AnnuaireEntreprise.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        #region Create (Ajout)
        /// <summary>
        /// Permet d'ajouter un employée
        /// </summary>
        /// <param name="employee">Objet "employee"</param>
        public void AddOneEmployee(Employee employee);
        #endregion

        #region Read (Lecture)
        /// <summary>
        /// Permet de récupérer un employée via son Id
        /// </summary>
        /// <param name="idEmployee">Id de l'employé recherché</param>
        /// <returns>Un objet "employee"</returns>
        public Employee GetOneEmployeeById(int idEmployee);

        /// <summary>
        /// Permet de récupérer une liste d'employées
        /// </summary>
        /// <param name="page">Numéro de page, par défaut page 1</param>
        /// <returns>Retourne une liste des employées</returns>
        public List<Employee> GetAllEmployees(int page = 1);

        /// <summary>
        /// Permet de récupérer la liste des salariés filtrés en fonction des champs remplis
        /// </summary>
        /// <param name="name">Nom de l'employée</param>
        /// <param name="service">Service de l'employée</param>
        /// <param name="site">Lieu de travail de l'employée</param>
        /// <returns>la liste des employées correspondant au filtre</returns>
        public List<Employee> GetAllEmployeesFiltered(string name, string service, string site);
        #endregion

        #region Update (Modification)
        /// <summary>
        /// Permet de modifier un employé
        /// </summary>
        /// <param name="employee">Objet "employee" à modifier</param>
        public void UpdateOneEmployee(Employee employee);
        #endregion

        #region Delete (Suppression)
        /// <summary>
        /// Permet de supprimer un employé via son Id
        /// </summary>
        /// <param name="idEmployee">Id de l'employé</param>
        public void DeleteOneEmployee(int idEmployee);
        #endregion
    }
}
