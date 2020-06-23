using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Contract.DTO
{
    public class OfferDrivingDTO
    {
        public int Id { get; set; }

        public int OfferRefId { get; set; }
        public OfferDTO Offer { get; set; }


        public int EmployeeRefId { get; set; }
        public EmployeeDTO Employee { get; set; }

        public DateTime TodaysDate { get; set; } //Brugeren skal ikke vælge noget her.

   
        public decimal Price { get; set; } //Brugeren skal ikke vælge noget her.
        public int Amount { get; set; }
        public int Rowversion { get; set; }
    }
}
