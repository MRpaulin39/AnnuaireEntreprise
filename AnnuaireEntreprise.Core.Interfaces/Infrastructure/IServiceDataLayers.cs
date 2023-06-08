using AnnuaireEntreprise.Core.Models;

namespace AnnuaireEntreprise.Core.Interfaces.Infrastructure
{
    public interface IServiceDataLayers
    {
        #region Create (Ajout)
        /// <summary>
        /// Permet d'ajouter un service
        /// </summary>
        /// <param name="service">Objet "service"</param>
        public void AddOneService(Service service);
        #endregion

        #region Read (Lecture)
        /// <summary>
        /// Permet de récupérer un service via son Id
        /// </summary>
        /// <param name="idService">Id du service recherché</param>
        /// <returns>Un objet "service"</returns>
        public Service GetOneServiceById(int idService);

        /// <summary>
        /// Permet de récupérer une liste de services
        /// </summary>
        /// <param name="page">Numéro de page, par défaut page 1</param>
        /// <returns>Retourne une liste des services</returns>
        public List<Service> GetAllServices(int page = 1);
        #endregion

        #region Update (Modification)
        /// <summary>
        /// Permet de modifier un service
        /// </summary>
        /// <param name="service">Objet "service" à modifier</param>
        public void UpdateOneService(Service service);
        #endregion

        #region Delete (Suppression)
        /// <summary>
        /// Permet de supprimer un service via son Id
        /// </summary>
        /// <param name="idService">Id du service</param>
        public void DeleteOneService(int idService);
        #endregion
    }
}
