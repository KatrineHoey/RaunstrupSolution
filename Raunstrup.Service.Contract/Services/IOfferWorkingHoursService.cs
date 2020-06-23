using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract.Services
{
    public interface IOfferWorkingHoursService
    {
        Task<IEnumerable<OfferWorkingHoursDTO>> GetOfferWorkingHoursAsync(int id);
        Task<OfferWorkingHoursDTO> GetOfferWorkingHourAsync(int id);
        Task AddAsync(OfferWorkingHoursDTO offerWorkingHours);
        Task UpdateAsync(int id, OfferWorkingHoursDTO offerWorkingHours);
        Task RemoveAsync(int id);
    }
}
