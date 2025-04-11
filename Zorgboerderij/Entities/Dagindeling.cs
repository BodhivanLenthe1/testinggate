using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zorgboerderij.Entities
{
    [Table("dpl")]
    public class Dagindeling
    {
        [Key]
        [Column("dpid")]
        public int Id { get; set; }

        [Column("dagid")]
        public string dagId { get; set; }

        [Column("bid")]
        public int? bid { get; set; }
        [ForeignKey("bid")]
        public Bakjes bakje { get; set; }

        [Column("kleur")]
        public string kleur { get; set; }

        [Column("soort")]
        public string soort { get; set; }

        [Column("datum")]
        public DateTime datum { get; set; }

        [Column("volgorde")]
        public int volgorde { get; set; }

        [Column("persid")]
        public int clientId { get; set; }
        [ForeignKey("clientId")]
        public Clienten client { get; set; }

        [Column("sid")]
        public int? sid { get; set; }

        [Column("sid2")]
        public int? sid2 { get; set; }
    }
}
