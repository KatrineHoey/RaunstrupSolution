using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public interface IOfferService
    {
        IEnumerable<Offer> GetOffers();
        Offer GetOffer(int id);
        void CreateOffer(Offer offer);
        void UpdateOffer(Offer offer);
        void UpdateOffer(int customerID, int offerID);
        void UpdateOfferProjectLeader(int employeeID, int offerID);
        void DeleteOffer(int id);
        void AddOfferEmployee(OfferEmployee offerEmployee);
        IEnumerable<OfferEmployee> GetOfferEmployee(int offerId);
        IEnumerable<Employee> GetEmployeesNotOnOffer(int offerId);
        void DeleteEmployeeFromOffer(int id);
        void AddDiscountToOffer(int offerId);

    }
}
