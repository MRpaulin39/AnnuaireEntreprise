using AnnuaireEntreprise.Core.Infrastructure.Databases;
using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireEntreprise.Core.Infrastructure.DataLayers
{
    public class EmployeeDataLayer : IEmployeeDataLayer
    {
        #region Propriétés
        private readonly AnnuaireDbContext _context;
        private readonly int _nombreResultatParPage = 25;
        #endregion

        #region Constructeur
        public EmployeeDataLayer()
        {
            _context = new AnnuaireDbContext();
        }
        #endregion


        #region Méthodes publiques
        #region Create (Ajout)
        public void AddOneEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }
        #endregion

        #region Read (Lecture)
        public List<Employee> GetAllEmployees(int page = 1)
        {
            return _context.Employees
                .Take(_nombreResultatParPage).Skip((page - 1) * _nombreResultatParPage)
                .ToList();
        }

        public Employee GetOneEmployeeById(int idEmployee)
        {
            return _context.Employees.Where(item => item.Id == idEmployee).Single();
        }
        #endregion

        #region Update (Modification)
        public void UpdateOneEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.Entry(employee.Service).State = EntityState.Unchanged;
            _context.Entry(employee.Site).State = EntityState.Unchanged;
            _context.SaveChanges();
        }
        #endregion

        #region Delete (Suppresion)
        public void DeleteOneEmployee(int idEmployee)
        {
            //Récupération de l'objet Employee associé à l'id
            Employee employee = _context.Employees.Where(item => item.Id == idEmployee).Single();

            _context.Remove(employee);
            _context.SaveChanges();
        }
        #endregion

        #endregion

    }
}
