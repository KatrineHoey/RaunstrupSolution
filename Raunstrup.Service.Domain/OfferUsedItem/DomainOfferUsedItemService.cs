using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Infrastructure.Database;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public class DomainOfferUsedItemService : IOfferUsedItemService
    {
        private readonly RaunstrupContext _context;

        public DomainOfferUsedItemService(RaunstrupContext context)
        {
            _context = context;
        }
        public void CreateOfferUsedItem(OfferUsedItem offerUsedItem)
        {
            if (_context.Offers.Any(c => c.Id == offerUsedItem.OfferRefId))
            {

                offerUsedItem.Rowversion = 1;
                _context.OfferUsedItems.Add(offerUsedItem);
                _context.SaveChanges();
            }

        }

        public void DeleteOfferUsedItem(int id)
        {
            if (_context.OfferUsedItems.Any(c => c.Id == id))
            {
                _context.OfferUsedItems.Remove(_context.OfferUsedItems.Find(id));
                _context.SaveChanges();
            }
        }

        public OfferUsedItem GetOfferUsedItem(int id)
        {
            return _context.OfferUsedItems
                .Where(w => w.Id == id)
                .Include(x => x.Employee)
            //    .Include(x => x.Item)
             .FirstOrDefault(); ;
        }

        public IEnumerable<OfferUsedItem> GetOfferUsedItems(int id)
        {
            return _context.OfferUsedItems
                .Where(w => w.OfferRefId == id)
                .Include(x => x.Employee)
              //  .Include(x => x.Item)
                .ToList();
        }

        public void UpdateOfferUsedItem(OfferUsedItem offerUsedItem)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.OfferUsedItems.Any(c => c.Id == offerUsedItem.Id)) //Tjekker at bilen stadig findes i databasen. 
            {
                offerUsedItem.Rowversion = offerUsedItem.Rowversion + 1; //Viser at der er blevet foretaget en ændring ved dette objekt. 
                _context.OfferUsedItems.Update(offerUsedItem);
                _context.SaveChanges();
            }
        }
    }
}
