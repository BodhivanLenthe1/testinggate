using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.Models
{
    public class Aanpasbaarheid
    {
        [Key]
        public int Id { get; set; }

        public string Logo { get; set; }
    }
}
