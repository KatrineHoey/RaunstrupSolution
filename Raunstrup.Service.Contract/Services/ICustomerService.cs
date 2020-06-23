using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
        Task<CustomerDTO> GetCustomerAsync(int id);
        Task AddAsync(CustomerDTO customer);
        Task UpdateAsync(int id, CustomerDTO customer);
        Task RemoveAsync(int id);
        Task<IEnumerable<CustomerDTO>> GetFilteredCustomersAsync(string searchString);
        Task UpdateAsync(int id, int offerId);
    }
}
