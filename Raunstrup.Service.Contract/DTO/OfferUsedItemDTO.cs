using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Contract.DTO
{
    public class OfferUsedItemDTO
    {
        public int Id { get; set; }

        public int OfferRefId { get; set; }
        public OfferDTO Offer { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public decimal OfferPrice { get; set; }
        public string MeasuringUnit { get; set; }

        public int Rowversion { get; set; }
        public int EmployeeRefId { get; set; }
        public EmployeeDTO Employee { get; set; }

        public int ItemRefId { get; set; }

        public ItemDTO Item { get; set; }
    }
}
