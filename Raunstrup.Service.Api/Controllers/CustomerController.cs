using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Api.Models;
using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Domain;

namespace Raunstrup.Service.Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly IOfferService _offerService;

        public CustomerController(ICustomerService customerService, IOfferService offerService)
        {
            _customerService = customerService;
            _offerService = offerService;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerDTO> GetCustomers()
        {
            return _customerService.GetCustomers().Select(c => CustomerMapper.Map(c));
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public CustomerDTO GetCustomer(int id)
        {
            if (_customerService.GetCustomer(id) == null)
            {
                CustomerDTO customerDTO = null;
                return customerDTO;
            }
            else
            {
                return CustomerMapper.Map(_customerService.GetCustomer(id));
            }

        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public void PutCustomer(int id, [FromBody]CustomerDTO customer)
        {
            _customerService.UpdateCustomer(CustomerMapper.Map(customer));

        }

        // POST: api/Customer
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public void PostCustomer([FromBody] CustomerDTO customer)
        {
            _customerService.CreateCustomer(CustomerMapper.Map(customer));

        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
        }

        [HttpGet("search/{searchString}", Name = "GetFilteredCustomers")]
        public IEnumerable<CustomerDTO> GetFilteredCustomers(string searchString)
        {
            return _customerService.GetFilteredCustomers(searchString).Select(a => CustomerMapper.Map(a));
        }

        [HttpPut("offer/{id}")]
        public void PutOffer(int id, [FromBody]int offerId)
        {
            _offerService.UpdateOffer(id, offerId);

        }
    }
}