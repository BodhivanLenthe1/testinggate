using System.ComponentModel;

namespace Zorgboerderij.Entities
{
    public class Afmeldingen
    {
        public int Id { get; set; }

        public string Client { get; set; }

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
