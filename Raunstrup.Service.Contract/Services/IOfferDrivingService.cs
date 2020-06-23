using Microsoft.EntityFrameworkCore.Update.Internal;
using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract.Services
{
    public interface IOfferDrivingService
    {
        Task<IEnumerable<OfferDrivingDTO>> GetOfferDrivingsAsync(int id);
        Task<OfferDrivingDTO> GetOfferDrivingAsync(int id);
        Task AddAsync(OfferDrivingDTO offerDriving);
        Task UpdateAsync(int id, OfferDrivingDTO offerDriving);
        Task RemoveAsync(int id);
    }
}
