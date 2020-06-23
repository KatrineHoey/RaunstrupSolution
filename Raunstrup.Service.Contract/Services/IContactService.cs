using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract
{
    public interface IContactService
    {
        Task<string> AddAsync(ContactDTO contact);
    }
}
