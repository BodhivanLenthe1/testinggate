using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.ViewModels
{
    public class ClientenViewModel
    {
        [Required]
        public string Voornaam { get; set; }

        [Required]
        public string Achternaam { get; set; }

        public IFormFile Foto { get; set; }

        [Required]
        public string Maandag { get; set; }

        [Required]
        public string Dinsdag { get; set; }

        [Required]
        public string Woensdag { get; set; }

        [Required]
        public string Donderdag { get; set; }

        [Required]
        public string Vrijdag { get; set; }

        [Required]
        public string Zaterdag { get; set; }

        [Required]
        public string Groepskleur { get; set; }
    }
}
