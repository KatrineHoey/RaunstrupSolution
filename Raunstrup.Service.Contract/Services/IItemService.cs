using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract.Services
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetItemsAsync();
        Task<ItemDTO> GetItemAsync(int id);
        Task AddAsync(ItemDTO item);
        Task UpdateAsync(int id, ItemDTO item);
        Task RemoveAsync(int id);
        Task<IEnumerable<ItemDTO>> GetFilteredItemsAsync(string searchString);
    }
}
