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
    [Route("api/[controller]")]
    [ApiController]
    public class OfferWorkingHoursController : ControllerBase
    {
        private readonly IOfferWorkingHoursService _OfferWorkingHoursService;

        public OfferWorkingHoursController(IOfferWorkingHoursService OfferWorkingHoursService)
        {
            _OfferWorkingHoursService = OfferWorkingHoursService;
        }

        // GET: api/OfferWorkingHours
        [HttpGet("all/{id}")]
        public IEnumerable<OfferWorkingHoursDTO> GetOfferWorkingHours(int id)
        {
            return _OfferWorkingHoursService.GetOfferWorkingHours(id).Select(i => OfferWorkingHoursMapper.Map(i));
            //return (IEnumerable<OfferWorkingHoursDTO>)OfferWorkingHoursMapper.Map(_OfferWorkingHoursService.GetOfferWorkingHours(id));
        }

        // GET: api/OfferWorkingHours/5
        [HttpGet("{id}")]
        public OfferWorkingHoursDTO GetOfferWorkingHour(int id)
        {
            if (_OfferWorkingHoursService.GetOfferWorkingHours(id) == null)
            {
                OfferWorkingHoursDTO OfferWorkingHoursDTO = null;
                return OfferWorkingHoursDTO;
            }
            else
            {
                return OfferWorkingHoursMapper.Map(_OfferWorkingHoursService.GetOfferWorkingHour(id));
            }

        }

        // PUT: api/OfferWorkingHours/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public void PutOfferWorkingHours(int id, [FromBody]OfferWorkingHoursDTO offerWorkingHours)
        {
            _OfferWorkingHoursService.UpdateOfferWorkingHours(OfferWorkingHoursMapper.Map(offerWorkingHours));

        }

        // POST: api/OfferWorkingHours
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public void PostOfferWorkingHours([FromBody] OfferWorkingHoursDTO offerWorkingHours)
        {
            _OfferWorkingHoursService.CreateOfferWorkingHours(OfferWorkingHoursMapper.Map(offerWorkingHours));

        }

        // DELETE: api/OfferWorkingHours/5
        [HttpDelete("{id}")]
        public void DeleteOfferWorkingHours(int id)
        {
            _OfferWorkingHoursService.DeleteOfferWorkingHours(id);
        }
    }
}