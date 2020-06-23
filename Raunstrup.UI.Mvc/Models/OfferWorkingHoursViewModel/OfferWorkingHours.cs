using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{ 
    public class OfferWorkingHours
    {
        public int Id { get; set; }
        [Required]
        public int OfferRefId { get; set; }
       
        public Offer Offer { get; set; }
        [Required]
        public int EmployeeRefId { get; set; }
        
        public Employee Employee { get; set; }
        [Required]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfWorking { get; set; } //Brugeren skal vælge dette.
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Indtast gyldigt nummer")]
        public int Amount { get; set; } //Brugeren skal vælge dette.
        //[Required]
    
        public decimal HourlyPrice { get; set; }
        public int Rowversion { get; set; }
    }
}
