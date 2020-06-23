using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raunstrup.Service.Infrastructure.Entities
{
    public class OfferWorkingHours
    {
        public int Id { get; set; }

        [ForeignKey("Offer")]
        public int OfferRefId { get; set; }
        public Offer Offer { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeRefId { get; set; }
        public Employee Employee { get; set; }

        public DateTime DateOfWorking { get; set; } //Brugeren skal vælge dette.

        public int Amount { get; set; } //Brugeren skal vælge dette.

        [Column(TypeName = "decimal(18,2)")]
        public decimal HourlyPrice { get; set; }

        public int Rowversion { get; set; }
    }
}
