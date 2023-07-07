using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Core.Models;

namespace AnnuaireEntreprise.Core.Application.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        #region Propriétés
        private readonly IServiceDataLayer _serviceDataLayers;
        #endregion

        #region Constructeur
        public ServiceRepository(IServiceDataLayer serviceDataLayers)
        {
            _serviceDataLayers = serviceDataLayers;
        }
        #endregion

        #region Méthodes publiques
        #region Create (Ajout)
        public void AddOneService(Service service)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read (Lecture)
        public List<Service> GetAllServices(int page = 1)
        {
            return _serviceDataLayers.GetAllServices(page);
        }

        public Service GetOneServiceById(int idService)
        {
            return _serviceDataLayers.GetOneServiceById(idService);
        }
        #endregion

        #region Update (Modification)
        public void UpdateOneService(Service service)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete (Suppression)
        public void DeleteOneService(int idService)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        #region Méthodes privées

        #endregion
    }
}
