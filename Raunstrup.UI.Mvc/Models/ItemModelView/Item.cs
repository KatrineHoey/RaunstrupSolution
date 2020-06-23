using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class Item
    {
        public int ID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public int ItemNo { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "VareNavn")]
        public string ItemName { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public double PurchasePrice { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public double SalePrice { get; set; }
        [Required]
        public string MeasuringUnit { get; set; }
        [Required]
        public bool Active { get; set; }
        public int RowVersion { get; set; }
    }
}
