using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Contract.Services;
using System.Text;
using Newtonsoft.Json.Schema;
using Microsoft.CodeAnalysis.Options;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Raunstrup.Service.Infrastructure.Entities;

namespace Raunstrup.UI.MVC.Services
{
    public class OfferEmployeeServiceProxy : IOfferEmployeeService

    {
        private const string _employeeRequestUri = "api/offeremployee";

        public HttpClient Client { get; }
        public OfferEmployeeServiceProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }


        // Medarbejder til offer
        public async Task AddEmployeeOffer(OfferEmployeeDTO offerEmployee)
        {
            var json = JsonSerializer.Serialize(offerEmployee);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"{_employeeRequestUri}", data).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }


        public async Task DeleteEmployeeAsync(int id)
        {
            var response = await Client.DeleteAsync($"{_employeeRequestUri}/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }


        public async Task<IEnumerable<OfferEmployeeDTO>> GetOfferEmployeeAsync(int id)
        {
            var response = await Client.GetAsync($"{_employeeRequestUri}/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<OfferEmployeeDTO>>(stream, options).ConfigureAwait(false);
        }



        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesNotOnOfferAsync(int id)
        {
            var response = await Client.GetAsync($"{_employeeRequestUri}/new/{id}").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions //Uvælger hvilke propterites som skal være gældene. 
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<IEnumerable<EmployeeDTO>>(stream, options).ConfigureAwait(false); //returnere en strøm af itemDTO'er og de er tællelige.
        }


    }
}
