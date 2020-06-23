using Microsoft.AspNetCore.Mvc.Rendering;
using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class OfferDriving
    {
        public int Id { get; set; }
        [Required]
        public int OfferRefId { get; set; }
        public Offer Offer { get; set; }
        [Display(Name = "Medarbejds-ID")]
        [Required]
        public int EmployeeRefId { get; set; }
        public Employee Employee { get; set; }
        [Required]
        [Display(Name = "Dato for kørsel")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TodaysDate { get; set; } //Brugeren skal ikke vælge noget her.
        public List<SelectListItem> DrivingType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "400", Text = "Dag" },
            new SelectListItem { Value = "4", Text = "Km" },
        };
        [Display(Name = "Pris")]
        public string Price { get; set; } //Brugeren skal ikke vælge noget her.
        [Display(Name = "Antal")]
        [Required]
        public int Amount { get; set; }
        public int Rowversion { get; set; }
    }
}
