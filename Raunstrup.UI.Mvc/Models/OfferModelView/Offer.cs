using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Raunstrup.UI.MVC.Models
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

        public int? ProjectleaderRefId { get; set; }
        public Employee Projectleader { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
        public int DiscountProcent { get; set; }

        public decimal TotalPriceWithDiscount { get; set; }

        public string Description { get; set; }

        public bool IsAccepted { get; set; }
        public bool PayForUsedItems { get; set; }

        public bool IsDone { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; } = true; //Sat defualt til true && Til sletning

        public IEnumerable<OfferAssignedItem> AssignedItems { get; set; } //Indeholder både materailer, timer og kørsel. 

        public IEnumerable<OfferUsedItem> UsedItems { get; set; } //Indeholder kun materialer der er forbrugt. 

        public IEnumerable<OfferWorkingHours> WorkingHours { get; set; }

        public IEnumerable<OfferDriving> ProjectDrivings { get; set; }

        public IEnumerable<OfferEmployee> ProjectEmployees { get; set; }

        public int Rowversion { get; set; }

    }
}
