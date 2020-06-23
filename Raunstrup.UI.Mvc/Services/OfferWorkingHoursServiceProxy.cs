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
    public class OfferWorkingHoursServiceProxy : IOfferWorkingHoursService
    {
        private const string _OfferWorkingHoursRequestUri = "api/OfferWorkingHours";
        public HttpClient Client { get; }
        public OfferWorkingHoursServiceProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }



        public async Task<IEnumerable<OfferWorkingHoursDTO>> GetOfferWorkingHoursAsync(int id) //tilbuds id
        {
            var response = await Client.GetAsync($"{_OfferWorkingHoursRequestUri}/all/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<OfferWorkingHoursDTO>>(stream, options).ConfigureAwait(false);
        }

        public async Task<OfferWorkingHoursDTO> GetOfferWorkingHourAsync(int id)
        {
            var response = await Client.GetAsync($"{_OfferWorkingHoursRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions //Uvælger hvilke propterites som skal være gældene. 
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<OfferWorkingHoursDTO>(stream, options).ConfigureAwait(false); //returnere en strøm af itemDTO'er og de er tællelige.

        }

        public async Task AddAsync(OfferWorkingHoursDTO offerWorkingHours)
        {
            var json = JsonSerializer.Serialize(offerWorkingHours); //Laver et objekt OfferWorkingHours om til json.
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_OfferWorkingHoursRequestUri, data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, OfferWorkingHoursDTO offerWorkingHours)
        {

            var json = JsonSerializer.Serialize(offerWorkingHours);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync($"{_OfferWorkingHoursRequestUri}/{id}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }


        public async Task RemoveAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_OfferWorkingHoursRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}
