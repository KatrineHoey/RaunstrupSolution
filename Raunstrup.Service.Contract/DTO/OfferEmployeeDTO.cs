using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Contract.DTO
{
    public class OfferEmployeeDTO
    {
        public int id { get; set; }

 
        public int OfferRefId { get; set; }
        public OfferDTO Offer { get; set; }

        public int EmployeeRefId { get; set; }
        public EmployeeDTO Employee { get; set; }

        public int Rowversion { get; set; }
    }
}
