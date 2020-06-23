using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raunstrup.Service.Infrastructure.Entities
{
    public class OfferEmployee
    {
        public int id { get; set; }

        [ForeignKey("Offer")]
        public int OfferRefId { get; set; }
        public Offer Offer { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeRefId { get; set; }
        public Employee Employee { get; set; }

        public int Rowversion { get; set; }

    }
}
