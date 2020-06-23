using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.Service.Api.Models
{
    public class OfferDrivingMapper
    {
        public static OfferDriving Map(OfferDrivingDTO dto)
        {
            return new OfferDriving
            {
                Id = dto.Id,
                OfferRefId = dto.OfferRefId,
                EmployeeRefId = dto.EmployeeRefId,
                Employee = EmployeeMapper.Map(dto.Employee),
                TodaysDate = dto.TodaysDate,
                Price = dto.Price,
                Amount = dto.Amount,
                Rowversion = dto.Rowversion

            };

        }

        public static IEnumerable<OfferDriving> Map(IEnumerable<OfferDrivingDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }
        public static IEnumerable<OfferDrivingDTO> Map(IEnumerable<OfferDriving> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static OfferDrivingDTO Map(OfferDriving view)
        {
            return new OfferDrivingDTO
            {
                Id = view.Id,
                OfferRefId = view.OfferRefId,
                EmployeeRefId = view.EmployeeRefId,
                Employee = EmployeeMapper.Map(view.Employee),
                TodaysDate = view.TodaysDate,
                Price = view.Price,
                Amount = view.Amount,
                Rowversion = view.Rowversion
            };
        }
    }
}
