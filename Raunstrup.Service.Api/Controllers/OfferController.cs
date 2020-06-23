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
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        // GET: api/Offer
        [HttpGet]
        public IEnumerable<OfferDTO> GetOffers()
        {
            return _offerService.GetOffers().Select(i => OfferMapper.Map(i));
        }

        // GET: api/Offer/5
        [HttpGet("{id}")]
        public OfferDTO GetOffer(int id)
        {
            if (_offerService.GetOffer(id) == null)
            {
                OfferDTO offerDTO = null;
                return offerDTO;
            }
            else
            {
                return OfferMapper.Map(_offerService.GetOffer(id));
            }

        }

        // PUT: api/Offer/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public void PutOffer(int id, [FromBody]OfferDTO offer)
        {
            _offerService.UpdateOffer(OfferMapper.Map(offer));

        }

        // POST: api/Offer
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public void PostOffer([FromBody] OfferDTO offer)
        {
            _offerService.CreateOffer(OfferMapper.Map(offer));

        }

        // DELETE: api/offer/5
        [HttpDelete("{id}")]
        public void DeleteOffer(int id)
        {
            _offerService.DeleteOffer(id);
        }


        // GET: api/Offer/5
        [HttpGet("{id}/Discount")]
        public void AddDiscountToOffer(int id)
        {
            _offerService.AddDiscountToOffer(id);

        }

    }
}