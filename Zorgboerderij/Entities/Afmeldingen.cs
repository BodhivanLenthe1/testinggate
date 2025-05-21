using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.Entities
{
    public class Afmeldingen
    {
        [Key]
        public int persid { get; set; }

        public string Tandarts { get; set; }

        public string Vakantie { get; set; }

        public string Verjaardag { get; set; }

        public string Ziek { get; set; }

        public string Vrij { get; set; }

        public string Dokter { get; set; }

        [DisplayName("Op karwei")]
        public string OpKarwei { get; set; }
    }
}
