using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raunstrup.Service.Api.Models
{
    class OfferWorkingHoursMapper
    {
        public static OfferWorkingHours Map(OfferWorkingHoursDTO dto)
        {
            return new OfferWorkingHours
            {
                Id = dto.Id,
                OfferRefId = dto.OfferRefId,
                Employee = EmployeeMapper.Map(dto.Employee),
                EmployeeRefId = dto.EmployeeRefId,
                DateOfWorking = dto.DateOfWorking,
                HourlyPrice = dto.HourlyPrice, 
                Amount = dto.Amount,
                Rowversion = dto.Rowversion
            };
        }

        public static IEnumerable<OfferWorkingHours> Map(IEnumerable<OfferWorkingHoursDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }
        public static IEnumerable<OfferWorkingHoursDTO> Map(IEnumerable<OfferWorkingHours> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static OfferWorkingHoursDTO Map(OfferWorkingHours view)
        {
            return new OfferWorkingHoursDTO
            {
                Id = view.Id,
                OfferRefId = view.OfferRefId,
                Employee = EmployeeMapper.Map(view.Employee),
                EmployeeRefId = view.EmployeeRefId,
                DateOfWorking = view.DateOfWorking,
                HourlyPrice = view.HourlyPrice,
                Amount = view.Amount,
                Rowversion = view.Rowversion

            };
        }
    }
}
