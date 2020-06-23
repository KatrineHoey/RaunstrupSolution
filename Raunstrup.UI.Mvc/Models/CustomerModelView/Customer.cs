using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class Customer
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 5)]
        public string Address { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string City { get; set; }
        [Required]
        public string DiscountGroup { get; set; }

        public List<SelectListItem> Discounts { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "5", Text = "5%" },
            new SelectListItem { Value = "10", Text = "10%" },
            new SelectListItem { Value = "15", Text = "15%"  },
            new SelectListItem { Value = "20", Text = "20%"  },
        };
        public int RowVersion {get; set;}
    }
}
