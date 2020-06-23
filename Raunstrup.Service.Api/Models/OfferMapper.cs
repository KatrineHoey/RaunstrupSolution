using Microsoft.AspNetCore.Mvc.ViewEngines;
using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.Service.Api.Models
{
    public static class OfferMapper
    {
        public static Offer Map(OfferDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new Offer
            {
                Id = dto.Id,
                WorkingTitle = dto.WorkingTitle,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Projectleader = EmployeeMapper.Map(dto.Projectleader),
                ProjectleaderRefId = dto.ProjectleaderRefId,
                IsActive = dto.IsActive,
                IsAccepted = dto.IsAccepted,
                PayForUsedItems = dto.PayForUsedItems,
                IsDone = dto.IsDone,
                TotalPrice = dto.TotalPrice,
                DiscountProcent = dto.DiscountProcent,
                TotalPriceWithDiscount = dto.TotalPriceWithDiscount,
                CustomerId = dto.CustomerId,
                Customer = CustomerMapper.Map(dto.Customer),
                AssignedItems = OfferAssignedItemMapper.Map(dto.AssignedItems),
                ProjectDrivings = OfferDrivingMapper.Map(dto.ProjectDrivings),
                ProjectEmployees = OfferEmployeeMapper.Map(dto.ProjectEmployees),
                UsedItems = OfferUsedItemMapper.Map(dto.UsedItems),
                WorkingHours = OfferWorkingHoursMapper.Map(dto.WorkingHours),
                Rowversion = dto.Rowversion
            };
        }

        public static IEnumerable<Offer> Map(IEnumerable<OfferDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static OfferDTO Map(Offer view)
        {
            if (view == null)
            {
                return null;
            }

            return new OfferDTO
            {
                Id = view.Id,
                WorkingTitle = view.WorkingTitle,
                Description = view.Description,
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                Projectleader = EmployeeMapper.Map(view.Projectleader),
                ProjectleaderRefId = view.ProjectleaderRefId,
                IsActive = view.IsActive,
                IsAccepted = view.IsAccepted,
                PayForUsedItems = view.PayForUsedItems,
                IsDone = view.IsDone,
                TotalPrice = view.TotalPrice,
                DiscountProcent = view.DiscountProcent,
                TotalPriceWithDiscount = view.TotalPriceWithDiscount,
                CustomerId = view.CustomerId,
                Customer = CustomerMapper.Map(view.Customer),
                AssignedItems = OfferAssignedItemMapper.Map(view.AssignedItems),
                ProjectDrivings = OfferDrivingMapper.Map(view.ProjectDrivings),
                ProjectEmployees = OfferEmployeeMapper.Map(view.ProjectEmployees),
                UsedItems = OfferUsedItemMapper.Map(view.UsedItems),
                WorkingHours = OfferWorkingHoursMapper.Map(view.WorkingHours),
                Rowversion = view.Rowversion
            };
        }
    }
}
