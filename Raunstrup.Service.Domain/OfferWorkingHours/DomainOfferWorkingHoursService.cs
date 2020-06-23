using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Infrastructure.Database;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public class DomainOfferWorkingHourservice : IOfferWorkingHoursService
    {
        private readonly RaunstrupContext _context;

        public DomainOfferWorkingHourservice(RaunstrupContext context)
        {
            _context = context;
        }
        public void CreateOfferWorkingHours(OfferWorkingHours offerWorkingHours)
        {
            if (_context.Offers.Any(c => c.Id == offerWorkingHours.OfferRefId))
            {
                Employee e = _context.Employees.Where(w => w.ID == offerWorkingHours.EmployeeRefId)
                    .Include(x => x.Profession)
                    .FirstOrDefault();

                offerWorkingHours.HourlyPrice = e.Profession.HourPrice;
                offerWorkingHours.Rowversion = 1;
                _context.WorkingHours.Add(offerWorkingHours);
                _context.SaveChanges();
            }

        }

        public void DeleteOfferWorkingHours(int id)
        {
            if (_context.WorkingHours.Any(c => c.Id == id))
            {
                _context.WorkingHours.Remove(_context.WorkingHours.Find(id));
                _context.SaveChanges();
            }
        }

        public OfferWorkingHours GetOfferWorkingHour(int id)
        {
            return _context.WorkingHours.Where(w => w.Id == id)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Profession)
             .FirstOrDefault(); ;
        }

        public IEnumerable<OfferWorkingHours> GetOfferWorkingHours(int offerId)
        {
            return _context.WorkingHours.Where(w => w.OfferRefId == offerId)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Profession)
                .ToList();
        }

        public void UpdateOfferWorkingHours(OfferWorkingHours OfferWorkingHours)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.WorkingHours.Any(c => c.Id == OfferWorkingHours.Id)) //Tjekker at bilen stadig findes i databasen. 
            {
                OfferWorkingHours.Rowversion = OfferWorkingHours.Rowversion + 1; //Viser at der er blevet foretaget en ændring ved dette objekt. 
                _context.WorkingHours.Update(OfferWorkingHours);
                _context.SaveChanges();
            }
        }
    }
}
