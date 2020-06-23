using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Infrastructure.Database;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public class DomainCustomerService : ICustomerService
    {
        private readonly RaunstrupContext _context;

        public DomainCustomerService(RaunstrupContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Find(id);
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Customers.Any(c => c.ID == customer.ID)) //Tjekker at kunden stadig findes i databasen. 
            {
                customer.RowVersion = customer.RowVersion + 1; //Viser at der er blevet foretaget en ændring ved denne kunde. 
                _context.Customers.Update(customer);
                _context.SaveChanges();
            }
        }
        public void CreateCustomer(Customer customer)
        {

            customer.RowVersion = 1;
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            if (_context.Customers.Any(c => c.ID == id))
            {
                _context.Customers.Remove(_context.Customers.Find(id));
                _context.SaveChanges();
            }
        }

        IEnumerable<Customer> ICustomerService.GetFilteredCustomers(string searchString)
        {

            return _context.Customers
                .Where(f => f.Name.ToUpper().Contains(searchString.ToUpper())
                || f.PhoneNo.ToString().Contains(searchString)
                || f.ID.ToString().Contains(searchString));


        }

        void ICustomerService.GetCustomerToOffer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

    }
}
