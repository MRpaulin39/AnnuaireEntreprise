using AnnuaireEntreprise.Core.Infrastructure.Databases;
using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireEntreprise.Core.Infrastructure.DataLayers
{
    public class ServiceDataLayer : IServiceDataLayer
    {
        #region Propriétés
        private readonly AnnuaireDbContext _context;
        private readonly int _nombreResultatParPage = 25;
        #endregion

        #region Constructeur
        public ServiceDataLayer()
        {
            _context = new AnnuaireDbContext();
        }
        #endregion


        #region Méthodes publiques
        #region Create (Ajout)
        public void AddOneService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }
        #endregion

        #region Read (Lecture)
        public List<Service> GetAllServices(int page = 1)
        {
            return _context.Services
                .Take(_nombreResultatParPage).Skip((page - 1) * _nombreResultatParPage)
                .ToList();
        }

        public Service GetOneServiceById(int idService)
        {
            return _context.Services.Where(item => item.Id == idService).Single();
        }

        public int CountEmployeeAssociateToTheService(int idService)
        {
            return _context.Employees.Where(item => item.Service.Id == idService).Count();
        }

        public bool CheckIfServiceAlreadyExist(string serviceName)
        {
            return _context.Services.Where(item => item.Name == serviceName).Any();
        }
        #endregion

        #region Update (Modification)
        public void UpdateOneService(Service service)
        {
            _context.ChangeTracker.Clear();

            _context.Entry(service).State = EntityState.Modified;
            _context.SaveChanges();
        }
        #endregion

        #region Delete (Suppresion)
        public void DeleteOneService(int idService)
        {
            //Récupération de l'objet Service associé à l'id
            Service service = _context.Services.Where(item => item.Id == idService).Single();

            _context.Remove(service);
            _context.SaveChanges();
        }        
        #endregion

        #endregion

    }
}
