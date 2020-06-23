using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raunstrup.Service.Contract.DTO
{
    public class OfferDTO
    {
        public OfferDTO()
        {
            WorkingHours = new List<OfferWorkingHoursDTO>();
            UsedItems = new List<OfferUsedItemDTO>();
            AssignedItems = new List<OfferAssignedItemDTO>();
            ProjectDrivings = new List<OfferDrivingDTO>();
            ProjectEmployees = new List<OfferEmployeeDTO>();
        }

        public int Id { get; set; }
        public string WorkingTitle { get; set; }
        public int? ProjectleaderRefId { get; set; }
        public EmployeeDTO Projectleader { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int DiscountProcent { get; set; }
        public decimal TotalPriceWithDiscount { get; set; }
        public string Description { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDone { get; set; }
        public bool PayForUsedItems { get; set; }
        public int? CustomerId { get; set; }
        public CustomerDTO Customer { get; set; }
        public bool IsActive { get; set; } = true; //Til sletning
        public IEnumerable<OfferAssignedItemDTO> AssignedItems { get; set; } //Indeholder både materailer, timer og kørsel. 

        public IEnumerable<OfferUsedItemDTO> UsedItems { get; set; } //Indeholder kun materialer der er forbrugt. 

        public IEnumerable<OfferWorkingHoursDTO> WorkingHours { get; set; }

        public IEnumerable<OfferDrivingDTO> ProjectDrivings { get; set; }

        public IEnumerable<OfferEmployeeDTO> ProjectEmployees { get; set; }

        public int Rowversion { get; set; }
    }
}
