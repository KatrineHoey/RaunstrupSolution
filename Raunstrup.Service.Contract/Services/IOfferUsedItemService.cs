using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract.Services
{
    public interface IOfferUsedItemService
    {
        Task<IEnumerable<OfferUsedItemDTO>> GetOfferUsedItemsAsync(int id);
        Task<OfferUsedItemDTO> GetOfferUsedItemAsync(int id);
        Task AddAsync(OfferUsedItemDTO OfferUsedItem);
        Task UpdateAsync(int id, OfferUsedItemDTO OfferUsedItem);
        Task RemoveAsync(int id);
    }
}
