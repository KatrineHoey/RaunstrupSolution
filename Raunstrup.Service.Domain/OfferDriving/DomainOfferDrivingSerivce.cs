using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Raunstrup.Service.Infrastructure.Database;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public class DomainOfferDrivingSerivce : IOfferDrivingService
    {
        private readonly RaunstrupContext _context;
        public DomainOfferDrivingSerivce(RaunstrupContext context)
        {
            _context = context;
        }


        public void CreateOfferDriving(OfferDriving offerDriving)
        {
            if (_context.Offers.Any(c => c.Id == offerDriving.OfferRefId))
            {
                offerDriving.Rowversion = 1;
                _context.OfferDrivings.Add(offerDriving);
                _context.SaveChanges();
            }
        }

        public void DeleteOfferDriving(int id)
        {
            if (_context.OfferDrivings.Any(c => c.Id == id)) 
            {
                _context.OfferDrivings.Remove(_context.OfferDrivings.Find(id));
                _context.SaveChanges();
            }
        }

        public OfferDriving GetOfferDriving(int id)
        {
            return _context.OfferDrivings.Where(c => c.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<OfferDriving> GetOfferDrivings(int offerId)
        {
            return _context.OfferDrivings.Where(c => c.OfferRefId == offerId)
                .ToList();
        }

        public void UpdateOfferDriving(OfferDriving offerDriving)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if (_context.OfferDrivings.Any(c => c.Id == offerDriving.Id))
            {
                offerDriving.Rowversion = offerDriving.Rowversion = +1;
                _context.OfferDrivings.Update(offerDriving);
                _context.SaveChanges();
            }
        }
    }
}
