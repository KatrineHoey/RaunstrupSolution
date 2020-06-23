using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raunstrup.Service.Infrastructure.Entities
{
    public class OfferAssignedItem
    {
        public int Id { get; set; }

        [ForeignKey("Offer")]
        public int OfferRefId { get; set; }
        public Offer Offer { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OfferPricePer { get; set; }
        public string MeasuringUnit { get; set; }

        public int Rowversion { get; set; }
    }
}
