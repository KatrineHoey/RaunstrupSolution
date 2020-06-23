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
    public class OfferUsedItemProxy : IOfferUsedItemService
    {
        private const string _OfferUsedItemRequestUri = "api/OfferUsedItem";
        public HttpClient Client { get; }
        public OfferUsedItemProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }



        public async Task<IEnumerable<OfferUsedItemDTO>> GetOfferUsedItemsAsync(int id) //tilbuds id
        {
            var response = await Client.GetAsync($"{_OfferUsedItemRequestUri}/all/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<OfferUsedItemDTO>>(stream, options).ConfigureAwait(false);
        }

        public async Task<OfferUsedItemDTO> GetOfferUsedItemAsync(int id)
        {
            var response = await Client.GetAsync($"{_OfferUsedItemRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions //Uvælger hvilke propterites som skal være gældende. 
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<OfferUsedItemDTO>(stream, options).ConfigureAwait(false); //returnerer en strøm af itemDTO'er og de er tællelige.

        }

        public async Task AddAsync(OfferUsedItemDTO offerUsedItem)
        {
            var json = JsonSerializer.Serialize(offerUsedItem); //Laver et objekt OfferUsedItem om til json.
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_OfferUsedItemRequestUri, data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, OfferUsedItemDTO offerUsedItem)
        {

            var json = JsonSerializer.Serialize(offerUsedItem);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{_OfferUsedItemRequestUri}/{id}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }


        public async Task RemoveAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_OfferUsedItemRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}
