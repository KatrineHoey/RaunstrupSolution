using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Infrastructure.Entities
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string Title { get; set; }
        public int Procent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Rowversion { get; set; }
    }
}
