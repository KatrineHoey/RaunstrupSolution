using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Contract.DTO
{
    public class OfferWorkingHoursDTO
    {
        public int Id { get; set; }

        public int OfferRefId { get; set; }
        public OfferDTO Offer { get; set; }


        public int EmployeeRefId { get; set; }
        public EmployeeDTO Employee { get; set; }

        public DateTime DateOfWorking { get; set; } //Brugeren skal vælge dette.

        public int Amount { get; set; } //Brugeren skal vælge dette.

        public decimal HourlyPrice { get; set; }

        public int Rowversion { get; set; }
    }
}
