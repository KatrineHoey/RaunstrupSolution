using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract.Services
{
    public interface IOfferService
    {
        Task<IEnumerable<OfferDTO>> GetOffersAsync();
        Task<OfferDTO> GetOfferAsync(int id);
        Task AddAsync(OfferDTO offer);
        Task UpdateAsync(int id, OfferDTO offer);
        Task RemoveAsync(int id);
        Task AddEmployeeToOfferAsync(OfferEmployeeDTO offerEmployee);

        Task AddDiscount(int id);

    }
}
