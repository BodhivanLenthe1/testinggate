using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zorgboerderij.Entities
{
    [Table("AfgerondeTaken")]
    public class AfgerondeTaak
    {
        [Key]
        [Column("taakid")]
        public int taakid { get; set; }

        [Column("bid")]
        public int bid { get; set; }

        [Column("persid")]
        public int persid { get; set; }

        [Column("sid")]
        public int? sid { get; set; }

        [Column("sid2")]
        public int? sid2 { get; set; }

        [Column("dag")]
        public string dag { get; set; }

        [Column("plandatum")]
        public DateTime plandatum { get; set; }

        [Column("datum_afronden")]
        public DateTime datum_afronden { get; set; }

        [Column("tijd_afronden")]
        public TimeSpan tijd_afronden { get; set; }
    }
}
