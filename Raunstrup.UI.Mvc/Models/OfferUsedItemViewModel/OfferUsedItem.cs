﻿using Microsoft.CodeAnalysis.CSharp;
using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class OfferUsedItem
    {
        public int Id { get; set; }
        [Required]
        public int OfferRefId { get; set; }
        //public Offer Offer { get; set; }
        //[Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public decimal OfferPrice { get; set; }
        [Required]
        public string MeasuringUnit { get; set; }
        public int Rowversion { get; set; }
        [Required]
        public int EmployeeRefId { get; set; }

        public Employee Employee { get; set; }
        //[Required]
 //       public int ItemRefId { get; set; }

        //Ekstra til view
        public Item Item { get; set; }
        public IEnumerable<Item> AllItems { get; set; }
    }
}
