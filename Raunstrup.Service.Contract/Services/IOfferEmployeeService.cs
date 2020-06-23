using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract.Services
{
    public interface IOfferEmployeeService
    {
        Task AddEmployeeOffer(OfferEmployeeDTO offeremployee);
        Task<IEnumerable<OfferEmployeeDTO>> GetOfferEmployeeAsync(int offerId);
        Task<IEnumerable<EmployeeDTO>> GetEmployeesNotOnOfferAsync(int offerId);
        Task DeleteEmployeeAsync(int id);
    }
}
