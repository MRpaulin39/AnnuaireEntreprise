namespace AnnuaireEntreprise.Core.Models
{
    /// <summary>
    /// Modèle Employee
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;

        public Service Service { get; set; }
        public Site Site { get; set; }
    }
}
