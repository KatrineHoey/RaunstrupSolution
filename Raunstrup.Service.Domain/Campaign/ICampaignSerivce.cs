using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Domain
{
    public interface ICampaignSerivce
    {
        IEnumerable<Campaign> GetAllCamaigns();
        Campaign GetCampaign(int id);
        void CreateCampaign(Campaign campaign);
        void UpdateCampaign(Campaign campaign);
        void DeleteCampaign(int id);
    }
}
