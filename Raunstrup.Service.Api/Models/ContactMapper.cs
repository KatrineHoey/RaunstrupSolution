using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.Service.Api.Models
{
    public class ContactMapper
    {
        public static Contact Map(ContactDTO dto)
        {
            return new Contact {Id = dto.Id, Name = dto.Name, Email = dto.Email, Subject = dto.Subject, Message = dto.Message };
        }

        public static IEnumerable<Contact> Map(IEnumerable<ContactDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static ContactDTO Map(Contact view)
        {
            return new ContactDTO
            {Id = view.Id, Name = view.Name, Email = view.Email, Subject = view.Subject, Message = view.Message };
        }
    }

}
