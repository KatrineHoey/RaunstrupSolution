using Microsoft.AspNetCore.Mvc.Rendering;
using Raunstrup.UI.MVC.Models.ProfessionModelView;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required]

        [Display(Name = "Cpr-nummer")]
        public int Cpr { get; set; }
        [Required]
        [Display(Name = "Fuldt navn")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefonnummer")]
        public int PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email-adresse")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Adresse")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Postnummer")]
        public int PostalCode { get; set; }
        [Required]
        [Display(Name = "Bynavn")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Projektleder")]
        public bool IsProjectleader { get; set; }

        [Required]
        [Display(Name = "Profession")]
        public string ProfessionRefID { get; set; }  

        public Profession Profession { get; set; }
        public List<SelectListItem> Professions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Udlært" },
            new SelectListItem { Value = "2", Text = "Lærling" },
            new SelectListItem { Value = "3", Text = "Projektleder"  },
            new SelectListItem { Value = "4", Text = "Kontor"  },
        };

        [Required]
        [Display(Name = "Specialisering")]
        public string Specialisation { get; set; }
        [Required]
        [Display(Name = "Brugernavn")]
        public string Username { get; set; } 
        public int RowVersion { get; set; }
    }
}
