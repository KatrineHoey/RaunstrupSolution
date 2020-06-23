using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raunstrup.Service.Infrastructure.Entities
{
    public class OfferUsedItem
    {
        public int Id { get; set; }

        [ForeignKey("Offer")]
        public int OfferRefId { get; set; }
        public Offer Offer { get; set; }
        
        [ForeignKey("Employee")]
        public int EmployeeRefId { get; set; }
        public Employee Employee { get; set; }

        
        //[ForeignKey("Item")]
        //public int ItemRefId { get; set; }
    //    public Item Item { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal OfferPrice { get; set; }
        public string MeasuringUnit { get; set; }

        public int Rowversion { get; set; }
    }
}
