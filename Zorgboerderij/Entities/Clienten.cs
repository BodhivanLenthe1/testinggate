using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Zorgboerderij.Entities
{
    public class Clienten
    {
        [Key]
        public int persid { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        [DisplayName("Foto")]
        public string? FotoFile { get; set; }

        public int Maandag { get; set; }

        public int Dinsdag { get; set; }

        public int Woensdag { get; set; }

        public int Donderdag { get; set; }

        public int Vrijdag { get; set; }

        public int Zaterdag { get; set; }

        public string Afwezig { get; set; }

        public string Goepskleur { get; set; }
    }
}
