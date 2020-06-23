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
    public class CustomerServiceProxy : ICustomerService
    {
        private const string _customerRequestUri = "api/Customer";
        public HttpClient Client { get; }
        public CustomerServiceProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }

        public async Task AddAsync(CustomerDTO customer)
        {
            var json = JsonSerializer.Serialize(customer); //Laver et objekt customer om til json.
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_customerRequestUri, data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<CustomerDTO> GetCustomerAsync(int id)
        {
            var response = await Client.GetAsync($"{_customerRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions //Uvælger hvilke propterites som skal være gældene. 
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<CustomerDTO>(stream, options).ConfigureAwait(false); //returnere en strøm af customerDTO'er og de er tællelige.

        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            var response = await Client.GetAsync(_customerRequestUri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<CustomerDTO>>(stream, options).ConfigureAwait(false);
        }

        public async Task RemoveAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_customerRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, CustomerDTO customer)
        {
            var json = JsonSerializer.Serialize(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{_customerRequestUri}/{id}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<CustomerDTO>> GetFilteredCustomersAsync(string searchString)
        {
            var response = await Client.GetAsync(_customerRequestUri + $"/search/{searchString}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<CustomerDTO>>(stream, options).ConfigureAwait(false);
        }



        public async Task UpdateAsync(int customerId, int offerId)
        {

            var json = JsonSerializer.Serialize(offerId);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{_customerRequestUri}/offer/{customerId}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
