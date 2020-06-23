using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Services
{
    public class CampaignServiceProxy : ICampaignService
    {
        private const string _CampaignRequestUri = "api/Campagin";
        public HttpClient Client { get; }
        public CampaignServiceProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }

        public async Task AddAsync(CampaignDTO campaign)
        {
            var json = JsonSerializer.Serialize(campaign);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_CampaignRequestUri, data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<CampaignDTO>> GetAllCampaignsAsync()
        {
            var response = await Client.GetAsync(_CampaignRequestUri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<CampaignDTO>>(stream, options).ConfigureAwait(false);
        }

        public async Task<CampaignDTO> GetCampaign(int id)
        {
            var response = await Client.GetAsync($"{_CampaignRequestUri}/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<CampaignDTO>(stream, options).ConfigureAwait(false);
        }

        public async Task RemoveAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_CampaignRequestUri}/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(CampaignDTO campaign, int id)
        {
            var json = JsonSerializer.Serialize(campaign);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{_CampaignRequestUri}/{id}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
