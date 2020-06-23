using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public interface IOfferUsedItemService
    {
        IEnumerable<OfferUsedItem> GetOfferUsedItems(int offerId);
        OfferUsedItem GetOfferUsedItem(int id);
        void CreateOfferUsedItem(OfferUsedItem offerUsedItem);
        void UpdateOfferUsedItem(OfferUsedItem offerUsedItem);
        void DeleteOfferUsedItem(int id);
    }
}
