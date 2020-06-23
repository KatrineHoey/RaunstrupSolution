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
    public class OfferAssignedItemServiceProxy : IOfferAssignedItemService
    {
        private const string _OfferAssignedItemRequestUri = "api/OfferAssignedItem";
        public HttpClient Client { get; }
        public OfferAssignedItemServiceProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }



        public async Task<IEnumerable<OfferAssignedItemDTO>> GetOfferAssignedItemsAsync(int id) //tilbuds id
        {
            var response = await Client.GetAsync($"{_OfferAssignedItemRequestUri}/all/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<OfferAssignedItemDTO>>(stream, options).ConfigureAwait(false);
        }

        public async Task<OfferAssignedItemDTO> GetOfferAssignedItemAsync(int id)
        {
            var response = await Client.GetAsync($"{_OfferAssignedItemRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions //Uvælger hvilke propterites som skal være gældene. 
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<OfferAssignedItemDTO>(stream, options).ConfigureAwait(false); //returnere en strøm af itemDTO'er og de er tællelige.

        }

        public async Task AddAsync(OfferAssignedItemDTO offerAssignedItem)
        {
            var json = JsonSerializer.Serialize(offerAssignedItem); //Laver et objekt OfferAssignedItem om til json.
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_OfferAssignedItemRequestUri, data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, OfferAssignedItemDTO offerAssignedItem)
        {

            var json = JsonSerializer.Serialize(offerAssignedItem);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{_OfferAssignedItemRequestUri}/{id}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }


        public async Task RemoveAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_OfferAssignedItemRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}
