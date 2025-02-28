using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.Entities
{
    public class Bakjes
    {
        [Key]
        public int bid { get; set; }

        public string Foto { get; set; }

        public string Titel { get; set; }

        public string Kleur { get; set; }
    }
}
