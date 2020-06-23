using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Api.Models;
using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.Service.Api.Controllers
{
    [Route("api/Campagin")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignSerivce _campaignSerivce;

        public CampaignController(ICampaignSerivce campaignSerivce)
        {
            _campaignSerivce = campaignSerivce;
        }

        // GET: api/Campaigns
        [HttpGet]
        public IEnumerable<CampaignDTO> GetAllCampaigns()
        {
            return _campaignSerivce.GetAllCamaigns().Select(c => CampaignMapper.Map(c));
        }


        // GET: api/Campaign/5
        [HttpGet("{id}")]
        public CampaignDTO GetCampaign(int id)
        {
            if (_campaignSerivce.GetCampaign(id) == null)
            {
                CampaignDTO campaignDTO = null;
                return campaignDTO;
            }
            else
            {
                return CampaignMapper.Map(_campaignSerivce.GetCampaign(id));
            }
        }

        // PUT: api/Campaigns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public void PutCampaign(int id, [FromBody] CampaignDTO campaign)
        {
            _campaignSerivce.UpdateCampaign(CampaignMapper.Map(campaign));

        }

        // POST: api/Campaign
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public void PostCustomer([FromBody] CampaignDTO campaign)
        {
            _campaignSerivce.CreateCampaign(CampaignMapper.Map(campaign));
        }

        // DELETE: api/Campaigns/5
        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            _campaignSerivce.DeleteCampaign(id);
        }
    }
}
