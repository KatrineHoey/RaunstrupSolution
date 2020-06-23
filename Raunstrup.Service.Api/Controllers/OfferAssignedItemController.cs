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
    [Route("api/OfferAssignedItem")]
    [ApiController]

    public class OfferAssignedItemController : ControllerBase
    {
        private readonly IOfferAssignedItemService _OfferAssignedItemService;

        public OfferAssignedItemController(IOfferAssignedItemService OfferAssignedItemService)
        {
            _OfferAssignedItemService = OfferAssignedItemService;
        }

        // GET: api/OfferAssignedItem
        [HttpGet ("all/{id}")]
        public IEnumerable<OfferAssignedItemDTO> GetOfferAssignedItems(int id)
        {
            return _OfferAssignedItemService.GetOfferAssignedItems(id).Select(i => OfferAssignedItemMapper.Map(i));
            //return (IEnumerable<OfferAssignedItemDTO>)OfferAssignedItemMapper.Map(_OfferAssignedItemService.GetOfferAssignedItem(id));
        }

        // GET: api/OfferAssignedItem/5
        [HttpGet("{id}")]
        public OfferAssignedItemDTO GetOfferAssignedItem(int id)
        {
            if (_OfferAssignedItemService.GetOfferAssignedItem(id) == null)
            {
                OfferAssignedItemDTO offerAssignedItemDTO = null;
                return offerAssignedItemDTO;
            }
            else
            {
                return OfferAssignedItemMapper.Map(_OfferAssignedItemService.GetOfferAssignedItem(id));
            }

        }

        // PUT: api/OfferAssignedItem/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public void PutOfferAssignedItem(int id, [FromBody]OfferAssignedItemDTO offerAssignedItem)
        {
            _OfferAssignedItemService.UpdateOfferAssignedItem(OfferAssignedItemMapper.Map(offerAssignedItem));

        }

        // POST: api/OfferAssignedItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public void PostOfferAssignedItem([FromBody] OfferAssignedItemDTO offerAssignedItem)
        {
            _OfferAssignedItemService.CreateOfferAssignedItem(OfferAssignedItemMapper.Map(offerAssignedItem));

        }

        // DELETE: api/OfferAssignedItem/5
        [HttpDelete("{id}")]
        public void DeleteOfferAssignedItem(int id)
        {
            _OfferAssignedItemService.DeleteOfferAssignedItem(id);
        }
    }
}