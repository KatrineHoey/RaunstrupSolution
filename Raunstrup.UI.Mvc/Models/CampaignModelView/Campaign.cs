using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class Campaign
    {
        [Display(Name = "Kampagne-ID")]
        public int CampaignId { get; set; }
        [Required]
        [Display(Name = "Kampagne-Titel")]
        public string Title { get; set; }
        [Display(Name = "Procenter %")]
        public int Procent { get; set; }
        [Required]
        [Display(Name = "Start-dato")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Slut-dato")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public int Rowversion { get; set; }
    }
}
