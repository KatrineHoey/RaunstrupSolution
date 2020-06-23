using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Infrastructure.Database;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public class DomainOfferAssignedItemService : IOfferAssignedItemService
    {

        private readonly RaunstrupContext _context;

        public DomainOfferAssignedItemService(RaunstrupContext context)
        {
            _context = context;
        }
        public void CreateOfferAssignedItem(OfferAssignedItem offerAssignedItem)
        {
            if(_context.Offers.Any(c => c.Id == offerAssignedItem.OfferRefId))
            {

                offerAssignedItem.Rowversion = 1;
                _context.OfferAssignedItems.Add(offerAssignedItem);
                _context.SaveChanges();
            }
          
        }

        public void DeleteOfferAssignedItem(int id)
        {
            if (_context.OfferAssignedItems.Any(c => c.Id == id))
            {
                _context.OfferAssignedItems.Remove(_context.OfferAssignedItems.Find(id));
                _context.SaveChanges();
            }
        }

        public OfferAssignedItem GetOfferAssignedItem(int id)
        {
            return _context.OfferAssignedItems.Where(w => w.Id == id)
             .FirstOrDefault(); ;
        }

        public IEnumerable<OfferAssignedItem> GetOfferAssignedItems(int id)
        {
            return _context.OfferAssignedItems.Where(w => w.OfferRefId == id)
                .ToList();
        }

        public void UpdateOfferAssignedItem(OfferAssignedItem offerAssignedItem)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.OfferAssignedItems.Any(c => c.Id == offerAssignedItem.Id)) //Tjekker at bilen stadig findes i databasen. 
            {
                offerAssignedItem.Rowversion = offerAssignedItem.Rowversion + 1; //Viser at der er blevet foretaget en ændring ved dette objekt. 
                _context.OfferAssignedItems.Update(offerAssignedItem);
                _context.SaveChanges();
            }
        }
    }
}
