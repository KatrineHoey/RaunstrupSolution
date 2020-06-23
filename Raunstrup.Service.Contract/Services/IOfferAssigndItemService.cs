using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract.Services
{
    public interface IOfferAssignedItemService
    {
        Task<IEnumerable<OfferAssignedItemDTO>> GetOfferAssignedItemsAsync(int id);
        Task<OfferAssignedItemDTO> GetOfferAssignedItemAsync(int id);
        Task AddAsync(OfferAssignedItemDTO OfferAssignedItem);
        Task UpdateAsync(int id, OfferAssignedItemDTO OfferAssignedItem);
        Task RemoveAsync(int id);
    }
}
