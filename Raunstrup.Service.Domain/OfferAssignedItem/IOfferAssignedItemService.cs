using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public interface IOfferAssignedItemService
    {
        IEnumerable<OfferAssignedItem> GetOfferAssignedItems(int id);
        OfferAssignedItem GetOfferAssignedItem(int id);
        void CreateOfferAssignedItem(OfferAssignedItem offerAssignedItem);
        void UpdateOfferAssignedItem(OfferAssignedItem offerAssignedItem);
        void DeleteOfferAssignedItem(int id);
    }
}
