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
    public class OfferUsedItemController : ControllerBase
    {
        private readonly IOfferUsedItemService _OfferUsedItemService;

        public OfferUsedItemController(IOfferUsedItemService OfferUsedItemService)
        {
            _OfferUsedItemService = OfferUsedItemService;
        }

        // GET: api/OfferUsedItem
        [HttpGet("all/{id}")]
        public IEnumerable<OfferUsedItemDTO> GetOfferUsedItems(int id)
        {
            return _OfferUsedItemService.GetOfferUsedItems(id).Select(i => OfferUsedItemMapper.Map(i));
            //return (IEnumerable<OfferUsedItemDTO>)OfferUsedItemMapper.Map(_OfferUsedItemService.GetOfferUsedItem(id));
        }

        // GET: api/OfferUsedItem/5
        [HttpGet("{id}")]
        public OfferUsedItemDTO GetOfferUsedItem(int id)
        {
            if (_OfferUsedItemService.GetOfferUsedItem(id) == null)
            {
                OfferUsedItemDTO offerUsedItemDTO = null;
                return offerUsedItemDTO;
            }
            else
            {
                return OfferUsedItemMapper.Map(_OfferUsedItemService.GetOfferUsedItem(id));
            }

        }

        // PUT: api/OfferUsedItem/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public void PutOfferUsedItem(int id, [FromBody]OfferUsedItemDTO offerUsedItem)
        {
            _OfferUsedItemService.UpdateOfferUsedItem(OfferUsedItemMapper.Map(offerUsedItem));

        }

        // POST: api/OfferUsedItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public void PostOfferUsedItem([FromBody] OfferUsedItemDTO offerUsedItem)
        {
            _OfferUsedItemService.CreateOfferUsedItem(OfferUsedItemMapper.Map(offerUsedItem));

        }

        // DELETE: api/OfferUsedItem/5
        [HttpDelete("{id}")]
        public void DeleteOfferUsedItem(int id)
        {
            _OfferUsedItemService.DeleteOfferUsedItem(id);
        }
    }
}