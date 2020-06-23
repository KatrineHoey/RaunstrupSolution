using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Contract.DTO
{
    public class ItemDTO
    {
        public int ID { get; set; }
        public int ItemNo { get; set; }
        public string ItemName { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public string MeasuringUnit { get; set; }
        public bool Active { get; set; }
        public int RowVersion { get; set; }
    }
}
