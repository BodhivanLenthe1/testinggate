using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.Models
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = "Org ID is verplicht.")]
        [MaxLength(50, ErrorMessage = "Max 50 karakters toegestaan.")]
        public string OrgId { get; set; }

        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [MaxLength(50, ErrorMessage = "Max 50 karakters toegestaan.")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [MaxLength(50, ErrorMessage = "Max 50 karakters toegestaan.")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Email is verplicht.")]
        [MaxLength(100, ErrorMessage = "Max 100 karakters toegestaan.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Voer een geldige email addres in.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
        [MaxLength(20, ErrorMessage = "Max 20 karakters toegestaan.")]
        public string Gebruikersnaam { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [MaxLength(20, ErrorMessage = "Max 20 karakters toegestaan.")]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht.")]
        [MaxLength(20, ErrorMessage = "Max 20 karakters toegestaan.")]
        [Compare("Wachtwoord", ErrorMessage = "De wachtwoorden komen niet overeen.")]
        [DataType(DataType.Password)]
        public string ConfirmWachtwoord { get; set; }
    }
}
