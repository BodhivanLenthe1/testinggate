using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zorgboerderij.Entities
{
    public class Dagcodes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Dag { get; set; }

        [Required]
        [DisplayName("Dag Code")]
        public string DagCode { get; set; }
    }
}
