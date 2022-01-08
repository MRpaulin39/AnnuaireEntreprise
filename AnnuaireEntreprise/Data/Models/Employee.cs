using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnnuaireEntreprise.Data.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Prénom requis requis")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nom de famille requis requis")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Numéro de téléphone fixe requis requis")]
        [MaxLength(10)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Numéro de téléphone mobile requis requis")]
        [MaxLength(10)]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Adresse email requis requis")]
        [MaxLength(120)]
        public string Mail { get; set; }

        //Clés étrangères
        public virtual Services Services { get; set; }
        public virtual Sites Sites { get; set; }


    }
}
