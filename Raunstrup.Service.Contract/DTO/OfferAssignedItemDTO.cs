using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Contract.DTO
{
    public class OfferAssignedItemDTO
    {
        public int Id { get; set; }

        public int OfferRefId { get; set; }
        public OfferDTO Offer { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public decimal OfferPricePer { get; set; }
        public string MeasuringUnit { get; set; }

        public int Rowversion { get; set; }
    }
}
