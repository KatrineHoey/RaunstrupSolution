using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public interface IOfferDrivingService
    {
        IEnumerable<OfferDriving> GetOfferDrivings(int offerId);
        OfferDriving GetOfferDriving(int id);
        void CreateOfferDriving(OfferDriving offerDriving);
        void UpdateOfferDriving(OfferDriving offerDriving);
        void DeleteOfferDriving(int id);
    }
}
