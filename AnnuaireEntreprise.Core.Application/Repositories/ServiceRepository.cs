using AnnuaireEntreprise.Core.CustomException.Repositories;
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
            CheckDataIntegrity(service, true);

            if (!_serviceDataLayers.CheckIfServiceAlreadyExist(service.Name))
            {
                _serviceDataLayers.AddOneService(service);
            }
            else
            {
                throw new ServiceRepositoryException("Ce service existe déjà");
            }

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
            CheckDataIntegrity(service, false);

            _serviceDataLayers.UpdateOneService(service);
        }
        #endregion

        #region Delete (Suppression)
        public void DeleteOneService(int idService)
        {
            if (_serviceDataLayers.CountEmployeeAssociateToTheService(idService) > 0)
            {
                throw new ServiceRepositoryException("Vous ne pouvez pas supprimer ce service, il est attribué à un ou plusieurs employés");
            }

            _serviceDataLayers.DeleteOneService(idService);
        }
        #endregion
        #endregion

        #region Méthodes privées
        private void CheckDataIntegrity(Service service, bool isNewService)
        {
            if (service == null) { throw new ServiceRepositoryException("Le service n'est pas initié"); }
            if (service.Name == string.Empty) { throw new ServiceRepositoryException("Veuillez entrer un nom de service"); }

            if (!isNewService)
            {
                if (service.Id <= 0) { throw new ServiceRepositoryException("L'id du service n'est pas défini !"); }
            }
        }
        #endregion
    }
}
