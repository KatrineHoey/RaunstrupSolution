using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.Service.Api.Models
{
    public static class OfferAssignedItemMapper
    {
        public static OfferAssignedItem Map(OfferAssignedItemDTO dto)
        {
            return new OfferAssignedItem
            {
                Id = dto.Id,
                Name = dto.Name,
                OfferRefId = dto.OfferRefId,
                Amount = dto.Amount,
                OfferPricePer = dto.OfferPricePer,
                MeasuringUnit = dto.MeasuringUnit,
                Rowversion = dto.Rowversion
            };
        }

        public static IEnumerable<OfferAssignedItem> Map(IEnumerable<OfferAssignedItemDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }
        public static IEnumerable<OfferAssignedItemDTO> Map(IEnumerable<OfferAssignedItem> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static OfferAssignedItemDTO Map(OfferAssignedItem view)
        {
            return new OfferAssignedItemDTO
            {
                Id = view.Id,
                Name = view.Name,
                OfferRefId = view.OfferRefId,
                Amount = view.Amount,
                OfferPricePer = view.OfferPricePer,
                MeasuringUnit = view.MeasuringUnit,
                Rowversion = view.Rowversion
            };
        }
    }
}
