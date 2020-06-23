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
    public class OfferDrivingProxy : IOfferDrivingService
    {
        private const string _offerDrivingRequestUri = "api/offerdriving";
        public HttpClient Client { get; }
        public OfferDrivingProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }

        public async Task AddAsync(OfferDrivingDTO offerDriving)
        {
            var json = JsonSerializer.Serialize(offerDriving);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_offerDrivingRequestUri, data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<OfferDrivingDTO> GetOfferDrivingAsync(int id)
        {
            var response = await Client.GetAsync($"{_offerDrivingRequestUri}/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<OfferDrivingDTO>(stream, options).ConfigureAwait(false); //returnere en strøm af itemDTO'er og de er tællelige.

        }

        public async Task<IEnumerable<OfferDrivingDTO>> GetOfferDrivingsAsync(int id)
        {
            var response = await Client.GetAsync($"{_offerDrivingRequestUri}/all/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<OfferDrivingDTO>>(stream, options).ConfigureAwait(false);
        }

        public async Task RemoveAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_offerDrivingRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, OfferDrivingDTO offerDriving)
        {
            var json = JsonSerializer.Serialize(offerDriving);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{_offerDrivingRequestUri}/{id}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
