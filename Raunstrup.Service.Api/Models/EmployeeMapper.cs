using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Infrastructure.Entities;

namespace Raunstrup.Service.Api.Models
{
    public static class EmployeeMapper
    {
        public static Employee Map(EmployeeDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new Employee
            {
                ID = dto.ID,
                Cpr = dto.Cpr,
                Name = dto.Name,
                Address = dto.Address,
                City = dto.City,
                PostalCode = dto.PostalCode,
                Email = dto.Email,
                PhoneNo = dto.PhoneNo,
                IsProjectleader = dto.IsProjectleader,
                ProfessionRefID = dto.ProfessionRefID,
                Profession = ProfessionMapper.Map(dto.Profession),
                Specialisation = dto.Specialisation,
                Username = dto.Username,
                RowVersion = dto.RowVersion
            };
        }
        public static IEnumerable<Employee> Map(IEnumerable<EmployeeDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static IEnumerable<EmployeeDTO> Map(IEnumerable<Employee> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static EmployeeDTO Map(Employee view)
        {
            if (view == null)
            {
                return null;
            }
            return new EmployeeDTO
            {
                ID = view.ID,
                Cpr = view.Cpr,
                Name = view.Name,
                Address = view.Address,
                City = view.City,
                PostalCode = view.PostalCode,
                Email = view.Email,
                PhoneNo = view.PhoneNo,
                IsProjectleader = view.IsProjectleader,
                ProfessionRefID = view.ProfessionRefID,
                Profession = ProfessionMapper.Map(view.Profession),
                Specialisation = view.Specialisation,
                Username = view.Username,
                RowVersion = view.RowVersion
            };
        }
    }
}
