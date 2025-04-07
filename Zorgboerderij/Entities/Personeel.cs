using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.Entities
{
    public class Personeel
    {
        [Key]
        public int persid { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        [DisplayName("Foto")]
        public string? FotoFile { get; set; }

        public string Maandag { get; set; }

        public string Dinsdag { get; set; }

        public string Woensdag { get; set; }

        public string Donderdag { get; set; }

        public string Vrijdag { get; set; }

        public string Zaterdag { get; set; }

        public string Groepskleur { get; set; }
    }
}
