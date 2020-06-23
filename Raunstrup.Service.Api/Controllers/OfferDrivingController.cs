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
    [Route("api/offerdriving")]
    [ApiController]
    public class OfferDrivingController : ControllerBase
    {
        private readonly IOfferDrivingService _offerDrivingService;

        public OfferDrivingController(IOfferDrivingService offerDrivingService)
        {
            _offerDrivingService = offerDrivingService;
        }

        // GET : api/OfferDriving
        [HttpGet("all/{id}")]
        public IEnumerable<OfferDrivingDTO> GetOfferDrivings(int id)
        {
            return _offerDrivingService.GetOfferDrivings(id).Select(i => OfferDrivingMapper.Map(i));
        }

        // GET : api/OfferDriving/5
        [HttpGet("{id}")]
        public OfferDrivingDTO GetOfferDriving(int id)
        {
            if (_offerDrivingService.GetOfferDriving(id) == null)
            {
                OfferDrivingDTO offerDrivingDTO = null;
                return offerDrivingDTO;
            }
            else
            {
                return OfferDrivingMapper.Map(_offerDrivingService.GetOfferDriving(id));
            }
        }

        // PUT: api/OfferDriving/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public void PutOfferDriving(int id, [FromBody]OfferDrivingDTO offerDriving)
        {
            _offerDrivingService.UpdateOfferDriving(OfferDrivingMapper.Map(offerDriving));

        }

        // POST: api/OfferDriving
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public void PostOfferDriving([FromBody] OfferDrivingDTO offerDriving)
        {
            _offerDrivingService.CreateOfferDriving(OfferDrivingMapper.Map(offerDriving));

        }

        // DELETE: api/OfferDriving/5
        [HttpDelete("{id}")]
        public void DeleteOfferDriving(int id)
        {
            _offerDrivingService.DeleteOfferDriving(id);
        }
    }
}