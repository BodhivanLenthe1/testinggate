using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Zorgboerderij.Entities
{
    public class Beschikbaarheid
    {
        [Key]
        public int Persid { get; set; } // Persoon ID

        [DisplayName("Maandag")]
        public string Maandag { get; set; } // Beschikbaarheid Maandag (bijv. O, M, H, X)

        [DisplayName("Dinsdag")]
        public string Dinsdag { get; set; } // Beschikbaarheid Dinsdag (bijv. O, M, H, X)

        [DisplayName("Woensdag")]
        public string Woensdag { get; set; } // Beschikbaarheid Woensdag (bijv. O, M, H, X)

        [DisplayName("Donderdag")]
        public string Donderdag { get; set; } // Beschikbaarheid Donderdag (bijv. O, M, H, X)

        [DisplayName("Vrijdag")]
        public string Vrijdag { get; set; } // Beschikbaarheid Vrijdag (bijv. O, M, H, X)

        [DisplayName("Zaterdag")]
        public string Zaterdag { get; set; } // Beschikbaarheid Zaterdag (bijv. O, M, H, X)


        [DisplayName("Groepskleur")]
        public string Groepskleur { get; set; } // Kan worden gebruikt om kleur te geven op basis van groepsstatus, indien van toepassing
    }
}
