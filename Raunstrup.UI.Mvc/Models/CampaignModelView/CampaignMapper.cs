using Raunstrup.Service.Contract.DTO;
using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class CampaignMapper
    {
        public static Campaign Map(CampaignDTO dto)
        {
            return new Campaign
            {
                CampaignId = dto.CampaignId,
                Title = dto.Title,
                Procent = dto.Procent,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rowversion = dto.Rowversion
            };
        }
        public static IEnumerable<Campaign> Map(IEnumerable<CampaignDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static IEnumerable<CampaignDTO> Map(IEnumerable<Campaign> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static CampaignDTO Map(Campaign view)
        {
            return new CampaignDTO
            {
                CampaignId = view.CampaignId,
                Title = view.Title,
                Procent = view.Procent,
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                Rowversion = view.Rowversion
            };
        }
    }
}
