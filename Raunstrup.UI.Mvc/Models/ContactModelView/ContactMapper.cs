using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models.ContactModelView
{
    public class ContactMapper
    {
        public static ContactMV Map(ContactDTO dto)
        {
            return new ContactMV
            {Id = dto.Id, Name = dto.Name, Email = dto.Email, Subject = dto.Subject, Message = dto.Message };
        }

        public static IEnumerable<ContactMV> Map(IEnumerable<ContactDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static ContactDTO Map(ContactMV view)
        {
            return new ContactDTO
            { Id = view.Id, Name = view.Name, Email = view.Email, Subject = view.Subject, Message = view.Message };
        }
    }
}
