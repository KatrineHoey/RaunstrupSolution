using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public interface IItemService
    {
        IEnumerable<Item> GetItems();
        Item GetItem(int id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(int id);
        IEnumerable<Item> GetFilteredItems(string searchString);
    }
}
