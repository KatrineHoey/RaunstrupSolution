using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class OfferEmployee
    {
        public int id { get; set; }
        [Required]
        public int OfferRefId { get; set; }
        [Required]
        public Offer Offer { get; set; }
        [Required]
        public int EmployeeRefId { get; set; }
        [Required]
        public Employee Employee { get; set; }

        public int Rowversion { get; set; }

    }
}
