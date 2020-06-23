using Raunstrup.Service.Contract.DTO;
using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{ 
    public class OfferUsedItemMapper
    {
        public static OfferUsedItem Map(OfferUsedItemDTO dto)
        {
            return new OfferUsedItem
            {
                Id = dto.Id,
                Name = dto.Name,
                OfferRefId = dto.OfferRefId,
                EmployeeRefId = dto.EmployeeRefId,
                Employee = EmployeeMapper.Map(dto.Employee),
      //          ItemRefId = dto.ItemRefId,
                Item = ItemMapper.Map(dto.Item),
                Amount = dto.Amount,
                OfferPrice = dto.OfferPrice,
                MeasuringUnit = dto.MeasuringUnit,
                Rowversion = dto.Rowversion
            };
        }

        public static IEnumerable<OfferUsedItem> Map(IEnumerable<OfferUsedItemDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static IEnumerable<OfferUsedItemDTO> Map(IEnumerable<OfferUsedItem> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static OfferUsedItemDTO Map(OfferUsedItem view)
        {
            return new OfferUsedItemDTO
            {
                Id = view.Id,
                Name = view.Name,
                OfferRefId = view.OfferRefId,
                EmployeeRefId = view.EmployeeRefId,
                Employee = EmployeeMapper.Map(view.Employee),
      //          ItemRefId = view.ItemRefId,
                Item = ItemMapper.Map(view.Item),
                Amount = view.Amount,
                OfferPrice = view.OfferPrice,
                MeasuringUnit = view.MeasuringUnit,
                Rowversion = view.Rowversion
            };
        }
    }
}
