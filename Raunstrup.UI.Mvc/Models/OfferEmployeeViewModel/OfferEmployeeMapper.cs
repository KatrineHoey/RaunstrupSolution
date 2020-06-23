using Raunstrup.Service.Contract.DTO;
using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class OfferEmployeeMapper
    {
        public static OfferEmployee Map(OfferEmployeeDTO dto)
        {
            return new OfferEmployee
            {
                id = dto.id,
                OfferRefId = dto.OfferRefId,
                Offer = OfferMapper.Map(dto.Offer),
                EmployeeRefId = dto.EmployeeRefId,
                Employee = EmployeeMapper.Map(dto.Employee),
                Rowversion = dto.Rowversion

            };

        }

        public static IEnumerable<OfferEmployee> Map(IEnumerable<OfferEmployeeDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static IEnumerable<OfferEmployeeDTO> Map(IEnumerable<OfferEmployee> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static OfferEmployeeDTO Map(OfferEmployee view)
        {
            return new OfferEmployeeDTO
            {
                id = view.id,
                OfferRefId = view.OfferRefId,
                Offer = OfferMapper.Map(view.Offer),
                EmployeeRefId = view.EmployeeRefId,
                Employee = EmployeeMapper.Map(view.Employee),
                Rowversion = view.Rowversion
            };
        }
    }
}
