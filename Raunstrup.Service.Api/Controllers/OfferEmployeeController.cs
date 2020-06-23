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
    [Route("api/offeremployee")]
    [ApiController]
    public class OfferEmployeeController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferEmployeeController(IOfferService offerService)
        {
        
            _offerService = offerService;
        }

        [HttpPost]
        public void PostOfferEmployee([FromBody] OfferEmployeeDTO offeremployee)
        {
            _offerService.AddOfferEmployee(OfferEmployeeMapper.Map(offeremployee));
        }

        [HttpGet("{id}")]
        public IEnumerable<OfferEmployeeDTO> GetOfferemployee(int id)
        {
            return OfferEmployeeMapper.Map(_offerService.GetOfferEmployee(id));
        }
        [HttpGet("new/{id}")]
        public IEnumerable<EmployeeDTO> GetEmployeeNoOffer(int id)
        {
            return EmployeeMapper.Map(_offerService.GetEmployeesNotOnOffer(id));
        }

        [HttpDelete("{id}")]
        public void DeleteEmployeeFromOffer(int id)
        {
            _offerService.DeleteEmployeeFromOffer(id);
        }
    }
}