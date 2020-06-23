using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
        void GetCustomerToOffer(Customer customer);
        IEnumerable<Customer> GetFilteredCustomers(string searchString);
    }
}
