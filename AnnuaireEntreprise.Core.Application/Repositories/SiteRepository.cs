using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Core.Models;

namespace AnnuaireEntreprise.Core.Application.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        #region Propriétés
        private readonly ISiteDataLayer _siteDataLayer;
        #endregion

        #region Constructeur
        public SiteRepository(ISiteDataLayer siteDataLayer)
        {
            _siteDataLayer = siteDataLayer;
        }
        #endregion

        #region Méthodes publiques 
        #region Create (Ajout)
        public void AddOneSite(Site site)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read (Lecture)
        public List<Site> GetAllSites(int page = 1)
        {
            throw new NotImplementedException();
        }

        public Site GetOneSiteById(int idSite)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update (Modification)
        public void UpdateOneSite(Site site)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete (Suppression)
        public void DeleteOneSite(int idSite)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region Méthodes privées 

        #endregion
    }
}
