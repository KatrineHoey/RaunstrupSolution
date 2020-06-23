using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raunstrup.Service.Infrastructure.Entities
{
    public class OfferDriving
    {
        public int Id { get; set; }

        [ForeignKey("Offer")]
        public int OfferRefId { get; set; }
        public Offer Offer { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeRefId { get; set; }
        public Employee Employee { get; set; }

        public DateTime TodaysDate { get; set; } //Brugeren skal ikke vælge noget her.

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } //Brugeren skal ikke vælge noget her.
        public int Amount { get; set; }
        public int Rowversion { get; set; }
    }
}
