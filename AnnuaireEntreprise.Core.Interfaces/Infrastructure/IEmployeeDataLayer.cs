﻿using AnnuaireEntreprise.Core.Models;

namespace AnnuaireEntreprise.Core.Interfaces.Infrastructure
{
    public interface IEmployeeDataLayer
    {
        #region Create (Ajout)
        /// <summary>
        /// Permet d'ajouter un employée dans la base de données
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