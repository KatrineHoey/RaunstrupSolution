using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;
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
    public class ItemServiceProxy : IItemService
    {
        private const string _itemRequestUri = "api/Item";
        public HttpClient Client { get; }
        public ItemServiceProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }

        public async Task AddAsync(ItemDTO item)
        {
            var json = JsonSerializer.Serialize(item); //Laver et objekt item om til json.
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_itemRequestUri, data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<ItemDTO> GetItemAsync(int id)
        {
            var response = await Client.GetAsync($"{_itemRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions //Uvælger hvilke propterites som skal være gældene. 
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<ItemDTO>(stream, options).ConfigureAwait(false); //returnere en strøm af itemDTO'er og de er tællelige.

        }

        public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
        {
            var response = await Client.GetAsync(_itemRequestUri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<ItemDTO>>(stream, options).ConfigureAwait(false);
        }

        public async Task RemoveAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_itemRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, ItemDTO item)
        {
            var json = JsonSerializer.Serialize(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{_itemRequestUri}/{id}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<ItemDTO>> GetFilteredItemsAsync(string searchString)
        {
            var response = await Client.GetAsync(_itemRequestUri + $"/search/{searchString}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<ItemDTO>>(stream, options).ConfigureAwait(false);
        }
    }
}
