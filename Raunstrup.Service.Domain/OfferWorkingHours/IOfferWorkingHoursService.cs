using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public interface IOfferWorkingHoursService
    {
        IEnumerable<OfferWorkingHours> GetOfferWorkingHours(int offerId);
        OfferWorkingHours GetOfferWorkingHour(int id);
        void CreateOfferWorkingHours(OfferWorkingHours offerWorkingHours);
        void UpdateOfferWorkingHours(OfferWorkingHours offerWorkingHours);
        void DeleteOfferWorkingHours(int id);
    }
}
