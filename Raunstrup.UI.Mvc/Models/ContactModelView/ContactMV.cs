using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models.ContactModelView
{
    public class ContactMV
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Subject { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Message { get; set; }
    }
}

