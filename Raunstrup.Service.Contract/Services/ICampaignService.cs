using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Raunstrup.Service.Contract.Services
{
    public interface ICampaignService
    {
        Task<IEnumerable<CampaignDTO>> GetAllCampaignsAsync();
        Task<CampaignDTO> GetCampaign(int id);
        Task AddAsync(CampaignDTO campaign);
        Task UpdateAsync(CampaignDTO campaign, int id);
        Task RemoveAsync(int id);
    }
}
