namespace AnnuaireEntreprise.ViewModels
{
    public class ReadEmployeeViewModels
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Mail { get; set; }

        //Clés étrangères
        public int ServicesId { get; set; }
        public string ServicesName { get; set; }
        public int SitesId { get; set; }
        public string SitesCity { get; set; }

        //Permet le filtre
        public string FirstAndLastName { get; set; }
    }

}
