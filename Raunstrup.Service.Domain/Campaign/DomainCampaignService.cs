using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Raunstrup.Service.Infrastructure.Database;
using Raunstrup.Service.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Raunstrup.Service.Domain
{
    public class DomainCampaignService : ICampaignSerivce
    {
        private readonly RaunstrupContext _Context;

        public DomainCampaignService(RaunstrupContext context)
        {
            _Context = context;
        }

        public void CreateCampaign(Campaign campaign)
        {
            campaign.Rowversion = 1;
            _Context.Campaigns.Add(campaign);
            _Context.SaveChanges();
        }

        public void DeleteCampaign(int id)
        {
            if (_Context.Campaigns.Any(c => c.CampaignId == id))
            {
                _Context.Campaigns.Remove(_Context.Campaigns.Find(id));
                _Context.SaveChanges();
            }
        }

        public IEnumerable<Campaign> GetAllCamaigns()
        {
            return _Context.Campaigns.ToList();
        }

        public Campaign GetCampaign(int id)
        {
            return _Context.Campaigns.Find(id);
        }

        public void UpdateCampaign(Campaign campaign)
        {
            _Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_Context.Campaigns.Any(c => c.CampaignId == campaign.CampaignId)) //Tjekker at tilbud stadig findes i databasen. 
            {
                campaign.Rowversion = campaign.Rowversion + 1; //Viser at der er blevet foretaget en ændring ved denne tilbud. 
                _Context.Campaigns.Update(campaign);
                _Context.SaveChanges();
            }
        }
    }
}
