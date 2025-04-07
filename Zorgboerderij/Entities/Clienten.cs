using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Zorgboerderij.Entities
{
    public class Clienten
    {
        [Key]
        public int persid { get; set; }

        [Required]
        public string Voornaam { get; set; }

        [Required]
        public string Achternaam { get; set; }

        [AllowNull]
        public string FotoFile { get; set; }
        [Required]
        public string Maandag { get; set; }
        [Required]
        public string Dinsdag { get; set; }
        [Required]
        public string Woensdag { get; set; }
        [Required]
        public string Donderdag { get; set; }
        [Required]
        public string Vrijdag { get; set; }
        [Required]
        public string Zaterdag { get; set; }
        [Required]
        public string Groepskleur { get; set; }
    }
}