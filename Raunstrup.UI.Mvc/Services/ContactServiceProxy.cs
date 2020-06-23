using Raunstrup.Service.Contract;
using Raunstrup.Service.Contract.DTO;
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
    public class ContactServiceProxy : IContactService
    {
        private const string _contactRequestUri = "api/contact";
        public HttpClient Client { get; }
        public ContactServiceProxy(HttpClient client)
        {
            Client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")
            { Parameters = { new NameValueHeaderValue("v", "1.0") } });
        }

        public async Task<string> AddAsync(ContactDTO contact)
        {
            var json = JsonSerializer.Serialize(contact); //Laver et objekt contact om til json.
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(_contactRequestUri, data).ConfigureAwait(false);
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode == true)
            {
                return "Tak fordi du kontaktede os, vi vender hurtigst muligt tilbage.";
            }
            else
            {
                return "Der skete desværre end fejl" + response;
            }
            
        }
    }
}
