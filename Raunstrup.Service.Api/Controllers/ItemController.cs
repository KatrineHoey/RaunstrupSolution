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
    [Route("api/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/Items
        [HttpGet]
        public IEnumerable<ItemDTO> GetItems()
        {
            return _itemService.GetItems().Select(i => ItemMapper.Map(i));
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public ItemDTO GetItem(int id)
        {
            if (_itemService.GetItem(id) == null)
            {
                ItemDTO itemDTO = null;
                return itemDTO;
            }
            else
            {
                return ItemMapper.Map(_itemService.GetItem(id));
            }

        }

        // PUT: api/Items/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public void PutItem(int id, [FromBody]ItemDTO item)
        {
            _itemService.UpdateItem(ItemMapper.Map(item));

        }

        // POST: api/Item
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public void PostItem([FromBody] ItemDTO item)
        {
            _itemService.CreateItem(ItemMapper.Map(item));

        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public void DeleteItem(int id)
        {
            _itemService.DeleteItem(id);
        }

        [HttpGet("search/{searchString}", Name = "GetFilteredItems")]
        public IEnumerable<ItemDTO> GetFilteredCustomers(string searchString)
        {
            return _itemService.GetFilteredItems(searchString).Select(a => ItemMapper.Map(a));
        }
    }
}