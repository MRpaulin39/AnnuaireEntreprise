using AnnuaireEntreprise.Core.Models;

namespace AnnuaireEntreprise.Core.Interfaces.Infrastructure
{
    public interface ISiteDataLayer
    {
        #region Create (Ajout)
        /// <summary>
        /// Permet d'ajouter un emplacement
        /// </summary>
        /// <param name="site">Objet "site"</param>
        public void AddOneSite(Site site);
        #endregion

        #region Read (Lecture)
        /// <summary>
        /// Permet de récupérer un emplacement via son Id
        /// </summary>
        /// <param name="idSite">Id de l'emplacement recherché</param>
        /// <returns>Un objet "employee"</returns>
        public Site GetOneSiteById(int idSite);

        /// <summary>
        /// Permet de récupérer une liste d'emplacements
        /// </summary>
        /// <param name="page">Numéro de page, par défaut page 1</param>
        /// <returns>Retourne une liste des emplacements</returns>
        public List<Site> GetAllSites(int page = 1);
        #endregion

        #region Update (Modification)
        /// <summary>
        /// Permet de modifier un emplacement
        /// </summary>
        /// <param name="employee">Objet "site" à modifier</param>
        public void UpdateOneSite(Site site);
        #endregion

        #region Delete (Suppression)
        /// <summary>
        /// Permet de supprimer un emplacement via son Id
        /// </summary>
        /// <param name="idSite">Id de l'emplacement</param>
        public void DeleteOneSite(int idSite);
        #endregion
    }
}
