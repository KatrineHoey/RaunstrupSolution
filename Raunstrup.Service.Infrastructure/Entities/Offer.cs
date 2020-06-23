using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raunstrup.Service.Infrastructure.Entities
{
    public class Offer
    {
        public Offer()
        {
            WorkingHours = new List<OfferWorkingHours>();
            UsedItems = new List<OfferUsedItem>();
            AssignedItems = new List<OfferAssignedItem>();
            ProjectDrivings = new List<OfferDriving>();
            ProjectEmployees = new List<OfferEmployee>();
        }

        public int Id { get; set; }
        public string WorkingTitle { get; set; }

        [ForeignKey("Projectleader")]
        public int? ProjectleaderRefId { get; set; }
        public Employee Projectleader { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public int DiscountProcent { get; set; }

        public decimal TotalPriceWithDiscount { get; set; }

        public string Description { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsDone { get; set; }

        public bool PayForUsedItems { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public bool IsActive { get; set; } = true; //Til sletning

        public IEnumerable<OfferAssignedItem> AssignedItems { get; set; } //Indeholder både materailer, timer og kørsel. 

        public IEnumerable<OfferUsedItem> UsedItems { get; set; } //Indeholder kun materialer der er forbrugt. 

        public IEnumerable<OfferWorkingHours> WorkingHours { get; set; }

        public IEnumerable<OfferDriving> ProjectDrivings { get; set; }

        public IEnumerable<OfferEmployee> ProjectEmployees { get; set; }

        public int Rowversion { get; set; }

    }
}
