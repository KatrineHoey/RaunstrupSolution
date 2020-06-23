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
    public class OfferServiceProxy : IOfferService
    {
        private const string _offerRequestUrl = "api/offer";
        public HttpClient Client { get; }
        public OfferServiceProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }

        public async Task<IEnumerable<OfferDTO>> GetOffersAsync()
        {
            var response = await Client.GetAsync(_offerRequestUrl).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<OfferDTO>>(stream, options).ConfigureAwait(false);
        }

        public async Task<OfferDTO> GetOfferAsync(int id)
        {
            var response = await Client.GetAsync($"{_offerRequestUrl}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions //Uvælger hvilke propterites som skal være gældene. 
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<OfferDTO>(stream, options).ConfigureAwait(false); //returnere en strøm af itemDTO'er og de er tællelige.

        }

        public async Task AddAsync(OfferDTO offer)
        {
            var json = JsonSerializer.Serialize(offer); //Laver et objekt offer om til json.
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_offerRequestUrl, data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, OfferDTO offer)
        {

            var json = JsonSerializer.Serialize(offer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{_offerRequestUrl}/{id}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }


        public async Task RemoveAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_offerRequestUrl}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task AddEmployeeToOfferAsync(OfferEmployeeDTO offerEmployee)
        {
            var json = JsonSerializer.Serialize(offerEmployee); //Laver et objekt offer om til json.
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_offerRequestUrl, data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }


        public async Task AddDiscount(int id)
        {
            var response = await Client.GetAsync($"{_offerRequestUrl}/{id}/Discount").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

    }
}
