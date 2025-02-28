using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Org ID is verplicht.")]
        [MaxLength(50, ErrorMessage = "Max 50 karakters toegestaan.")]
        [DisplayName("Org ID")]
        public string OrgId { get; set; }

        [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
        [MaxLength(20, ErrorMessage = "Max 20 karakters toegestaan.")]
        public string Gebruikersnaam { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [MaxLength(20, ErrorMessage = "Max 20 karakters toegestaan.")]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
    }
}
