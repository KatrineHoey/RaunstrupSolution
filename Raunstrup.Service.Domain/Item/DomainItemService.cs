using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Infrastructure.Database;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public class DomainItemService : IItemService
    {
        private readonly RaunstrupContext _context;

        public DomainItemService(RaunstrupContext context)
        {
            _context = context;
        }

        public IEnumerable<Item> GetItems()
        {
            return _context.Items
                .Where(i => i.Active == true)
                .ToList();
        }

        public Item GetItem(int id)
        {
            return _context.Items.Find(id);
        }

        public void UpdateItem(Item item)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Items.Any(c => c.ID == item.ID)) //Tjekker at bilen stadig findes i databasen. 
            {
                item.RowVersion = item.RowVersion + 1; //Viser at der er blevet foretaget en ændring ved dette objekt. 
                _context.Items.Update(item);
                _context.SaveChanges();
            }
        }

        public void CreateItem(Item item)
        {
            item.Active = true;
            item.RowVersion = 1;
            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Offers.Any(c => c.Id == id))
            {
                Item item = _context.Items.Find(id);
                item.Active = false;
                item.RowVersion = item.RowVersion + 1;
                _context.Items.Update(item);
                _context.SaveChanges();
            }
        }

        IEnumerable<Item> IItemService.GetFilteredItems(string searchString)
        {

            return _context.Items
                .Where(f => f.Active == true)
                .Where(f => f.ItemName.ToUpper().Contains(searchString.ToUpper())
                || f.ItemNo.ToString().Contains(searchString));


        }
    }
}
