using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Gebruikersnaam), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Org ID is verplicht.")]
        [MaxLength(50, ErrorMessage = "Max 50 karakters toegestaan.")]
        [DisplayName("Org ID")]
        public string OrgId { get; set; }

        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [MaxLength(50, ErrorMessage = "Max 50 karakters toegestaan.")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [MaxLength(50, ErrorMessage = "Max 50 karakters toegestaan.")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Email is verplicht.")]
        [MaxLength(100, ErrorMessage = "Max 100 karakters toegestaan.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
        [MaxLength(20, ErrorMessage = "Max 20 karakters toegestaan.")]
        public string Gebruikersnaam { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [MaxLength(20, ErrorMessage = "Max 20 karakters toegestaan.")]
        public string Wachtwoord { get; set; }
    }
}
