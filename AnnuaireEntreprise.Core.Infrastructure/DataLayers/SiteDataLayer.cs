using AnnuaireEntreprise.Core.Infrastructure.Databases;
using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireEntreprise.Core.Infrastructure.DataLayers
{
    public class SiteDataLayer : ISiteDataLayer
    {
        #region Propriétés
        private readonly AnnuaireDbContext _context;
        private readonly int _nombreResultatParPage = 25;
        #endregion

        #region Constructeur
        public SiteDataLayer(AnnuaireDbContext context)
        {
            _context = context;
        }
        #endregion


        #region Méthodes publiques
        #region Create (Ajout)
        public void AddOneSite(Site site)
        {
            _context.Sites.Add(site);
            _context.SaveChanges();
        }
        #endregion

        #region Read (Lecture)
        public List<Site> GetAllSites(int page = 1)
        {
            return _context.Sites
                .Take(_nombreResultatParPage).Skip((page - 1) * _nombreResultatParPage)
                .ToList();
        }

        public Site GetOneSiteById(int idSite)
        {
            return _context.Sites.Where(item => item.Id == idSite).Single();
        }
        #endregion

        #region Update (Modification)
        public void UpdateOneSite(Site site)
        {
            _context.Entry(site).State = EntityState.Modified;
            _context.SaveChanges();
        }
        #endregion

        #region Delete (Suppresion)
        public void DeleteOneSite(int idSite)
        {
            //Récupération de l'objet Site associé à l'id
            Site site = _context.Sites.Where(item => item.Id == idSite).Single();

            _context.Remove(site);
            _context.SaveChanges();
        }
        #endregion

        #endregion

    }
}
