using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Gebruikersnaam), IsUnique = true)]
    public class UserAccount
    {
        public int Id { get; set; }
        public string OrgId { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        public string? Toegang { get; set; }
        public DateTime? LaatstIngelogd { get; set; }
    }
}
