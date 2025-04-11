using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Zorgboerderij.Models
{
    public class AanpasbaarheidViewModel
    {
        [Required(ErrorMessage = "Voer een begintijd in.")]
        [Display(Name = "Begintijd")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "Voer een eindtijd in.")]
        [Display(Name = "Eindtijd")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Logo uploaden (optioneel)")]
        public IFormFile LogoFile { get; set; }
        public string ExistingLogoUrl { get; set; }
    }
}
