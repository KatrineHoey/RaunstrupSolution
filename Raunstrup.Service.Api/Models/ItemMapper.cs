using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Raunstrup.Service.Api.Models
{
    public class ItemMapper
    {
        public static Item Map(ItemDTO dto)
        {
            if (dto == null)
            {
                return null;
            }
            return new Item
            { ID = dto.ID, ItemNo = dto.ItemNo, ItemName = dto.ItemName, PurchasePrice = dto.PurchasePrice, SalePrice = dto.SalePrice, MeasuringUnit = dto.MeasuringUnit,Active = dto.Active, RowVersion = dto.RowVersion };
        }

        public static IEnumerable<Item> Map(IEnumerable<ItemDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static ItemDTO Map(Item view)
        {
            if (view == null)
            {
                return null;
            }
            return new ItemDTO
            { ID = view.ID, ItemNo = view.ItemNo, ItemName = view.ItemName, PurchasePrice = view.PurchasePrice, SalePrice = view.SalePrice, MeasuringUnit = view.MeasuringUnit,Active = view.Active, RowVersion = view.RowVersion };
        }
    }
}
