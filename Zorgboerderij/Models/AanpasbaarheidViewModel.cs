using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Zorgboerderij.Models
{
    public class AanpasbaarheidViewModel
    {
        [Display(Name = "Logo uploaden (optioneel)")]
        public IFormFile LogoFile { get; set; }

        public string ExistingLogo { get; set; }
    }
}
