using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Infrastructure.Database;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public class DomainOfferService : IOfferService
    {
        private readonly RaunstrupContext _context;

        public DomainOfferService(RaunstrupContext context)
        {
            _context = context;
        }

        public IEnumerable<Offer> GetOffers()
        {
            return _context.Offers
                .Where(o => o.IsActive == true)
                //.Include(o => o.ProjectEmployees)
                //.ThenInclude(e => e.Employee)
                .Include(o => o.AssignedItems)
                .Include(o => o.UsedItems)
                .ThenInclude(o => o.Employee)
                .Include(o => o.WorkingHours)
                .ThenInclude(o => o.Employee)
                .Include(o => o.ProjectDrivings)
                .ThenInclude(d => d.Employee)
                .Include(c => c.Customer)
                .ToList();


        }

        public Offer GetOffer(int id)
        {
            Offer offer =  _context.Offers
                .Include(o=> o.Projectleader)
                //.Include(o => o.ProjectEmployees)
                //.ThenInclude(e => e.Employee)
                .Include(o => o.AssignedItems)
                .Include(o => o.UsedItems)
                .ThenInclude(o => o.Employee)
                .Include(o => o.WorkingHours)
                .ThenInclude(o => o.Employee)
                .Include(o => o.ProjectDrivings)
                .ThenInclude(d => d.Employee)
                .Include(c => c.Customer)
                .FirstOrDefault(x => x.Id == id); //Henter den første instans som passer med id'et.

            offer = CalculateTotal(offer);
            return offer;
        }

        public void UpdateOffer(Offer offer)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Offers.Any(c => c.Id == offer.Id)) //Tjekker at bilen stadig findes i databasen. 
            {
                offer.Rowversion = offer.Rowversion + 1; //Viser at der er blevet foretaget en ændring ved dette objekt. 
                _context.Offers.Update(offer);
                _context.SaveChanges();
            }
        }

        public void UpdateOffer(int customerID, int offerId)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Offers.Any(c => c.Id == offerId)) //Tjekker at bilen stadig findes i databasen. 
            {
                Offer offer = _context.Offers.Find(offerId);
                offer.CustomerId = customerID;
                offer.Rowversion = offer.Rowversion + 1; //Viser at der er blevet foretaget en ændring ved dette objekt. 
                _context.Offers.Update(offer);
                _context.SaveChanges();
            }
        }

        public void UpdateOfferProjectLeader(int employeeID, int offerId)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Offers.Any(c => c.Id == offerId)) //Tjekker at bilen stadig findes i databasen. 
            {
                Offer offer = _context.Offers.Find(offerId);
                offer.ProjectleaderRefId = employeeID;
                offer.Rowversion = offer.Rowversion + 1; //Viser at der er blevet foretaget en ændring ved dette objekt. 
                _context.Offers.Update(offer);
                _context.SaveChanges();
            }
        }

        public void CreateOffer(Offer offer)
        {
            offer.IsActive = true;
            offer.IsDone = false;
            offer.Rowversion = 1;
            _context.Offers.Add(offer);
            _context.SaveChanges();

        }

        public void DeleteOffer(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Offers.Any(c => c.Id == id))
            {
                Offer offer = _context.Offers.Find(id);
                offer.IsActive = false;
                offer.Rowversion = offer.Rowversion + 1;
                _context.Offers.Update(offer);
                _context.SaveChanges();
            }
        }

        public void AddOfferEmployee(OfferEmployee offerEmployee)
        {
            if (_context.Offers.Any(c => c.Id == offerEmployee.OfferRefId))
            {
                offerEmployee.Rowversion = 1;
                _context.OfferEmployees.Add(offerEmployee);
                _context.SaveChanges();
            }
        }

        public IEnumerable<OfferEmployee> GetOfferEmployee(int offerId)
        {
            return _context.OfferEmployees
                .Where(o => o.OfferRefId == offerId)
                .Include(e => e.Employee)
                .ThenInclude(x => x.Profession);


        }
        public IEnumerable<Employee> GetEmployeesNotOnOffer(int offerId)
        {
            //vælg alt fra employee som ikke eksisterer i offeremployee
  
            return _context.Employees
                .Where(c => !_context.OfferEmployees.Any(p => p.OfferRefId == offerId && p.EmployeeRefId == c.ID))
                .Include(x=> x.Profession);

        }

        // Søger efter Id i OfferEmployee tablen og sletter medabrjderen som er tilknyttet til tilbudet
        public void DeleteEmployeeFromOffer(int id)
        {
            if (_context.OfferEmployees.Any(c => c.id == id))
            {
                _context.OfferEmployees.Remove(_context.OfferEmployees.Find(id));
                _context.SaveChanges();
            }
        }


        public void AddDiscountToOffer(int offerId)
        {
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Offers.Any(c => c.Id == offerId)) //Tjekker at bilen stadig findes i databasen. 
            {
                Offer offer = _context.Offers
                    .Include(o => o.Projectleader)
                    .Include(o => o.AssignedItems)
                    .Include(o => o.UsedItems)
                    .ThenInclude(o => o.Employee)
                    .Include(o => o.WorkingHours)
                    .ThenInclude(o => o.Employee)
                    .Include(o => o.ProjectDrivings)
                    .ThenInclude(d => d.Employee)
                    .Include(c => c.Customer)
                    .FirstOrDefault(x => x.Id == offerId); //Henter den første instans som passer med id'et.

                int campaignDiscount = 0;

                DateTime today = DateTime.UtcNow;
                var campaigns = _context.Campaigns
                    .Where(x => x.StartDate <= today && x.EndDate >= today);

                foreach (var item in campaigns)
                {
                    if (campaignDiscount < item.Procent)
                    {
                        campaignDiscount = item.Procent;
                    }
                }

                if (offer.Customer != null)
                {
                    int customerDiscount = offer.Customer.DiscountGroup;     

                    if (customerDiscount > campaignDiscount)
                    {
                        offer.DiscountProcent = customerDiscount;
                    }
                    else
                    {
                        offer.DiscountProcent = campaignDiscount;
                    }

                }
                else
                {
                    offer.DiscountProcent = campaignDiscount;
                }

                offer.Rowversion = offer.Rowversion + 1; //Viser at der er blevet foretaget en ændring ved dette objekt. 
                _context.Offers.Update(offer);
                _context.SaveChanges();
            }
        }

        public Offer CalculateTotal(Offer offer)
        {
            if (offer.PayForUsedItems == true)
            {
                decimal sumItems = 0;
                if (offer.UsedItems.Count() > 0)
                {
                    foreach (var item in offer.UsedItems)
                    {
                        sumItems = sumItems + (item.Amount * item.OfferPrice);
                    }
                }

                decimal sumHours = 0;
                if (offer.WorkingHours.Count() > 0)
                {
                    int count = 0;
                    foreach (var item in offer.WorkingHours)
                    {
                        sumHours = sumHours + (item.Amount * item.HourlyPrice);
                        count = count + item.Amount;
                    }

                }

                decimal sumDriving = 0;
                if (offer.ProjectDrivings.Count() > 0)
                {
                    foreach (var item in offer.ProjectDrivings)
                    {
                        sumDriving = sumDriving + (item.Amount * item.Price);
                    }
                }

                offer.TotalPrice = sumItems + sumHours + sumDriving;
            }
            else
            {
                decimal sumItems = 0;
                if (offer.AssignedItems.Count() > 0)
                {
                    foreach (var item in offer.AssignedItems)
                    {
                        sumItems = sumItems + (item.Amount * item.OfferPricePer);
                    }
                }

                offer.TotalPrice = sumItems;
            }
            
            if (offer.DiscountProcent > 1) //Tjek at der er en rabat. 
            {
                offer.TotalPriceWithDiscount = offer.TotalPrice - (offer.TotalPrice / 100 * offer.DiscountProcent);

            }
            else
            {
                offer.TotalPriceWithDiscount = offer.TotalPrice;
            }
            _context.Offers.Update(offer);

            return offer;
        }
    }
}
