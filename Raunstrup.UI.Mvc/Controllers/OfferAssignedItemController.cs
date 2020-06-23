using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;

namespace Raunstrup.UI.MVC.Controllers
{
    public class OfferAssignedItemController : Controller
    {
        private readonly IOfferAssignedItemService _OfferAssignedItemService;
        private readonly IItemService _itemService;
        private static int offerID;
        public OfferAssignedItemController(IOfferAssignedItemService OfferAssignedItemService, IItemService itemService)
        {
            _OfferAssignedItemService = OfferAssignedItemService;
            _itemService = itemService;
        }

        //Get: OfferAssignedItems
        public async Task<IActionResult> Index(int id)
        {
            offerID = id;
            var offerAssignedItemDtos = await _OfferAssignedItemService.GetOfferAssignedItemsAsync(id).ConfigureAwait(false);
            return View(OfferAssignedItemMapper.Map(offerAssignedItemDtos));

        }

        // Get : OfferAssignedItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var OfferAssignedItem = await _OfferAssignedItemService.GetOfferAssignedItemAsync(id.Value).ConfigureAwait(false);
                if (OfferAssignedItem == null)
                {
                    return NotFound();
                }
                return View(OfferAssignedItemMapper.Map(OfferAssignedItem));  
            }
            catch (Exception)
            {
                TempData["OfferAssignedItemCantBeFound"] = "Linjen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: OfferAssignedItems/Create
        public async Task<IActionResult> Create(string searchString)
        {

            // Hent alle materialer
            OfferAssignedItem offerAssignedItem = new OfferAssignedItem();
 
            if (!String.IsNullOrEmpty(searchString))
            {
                offerAssignedItem.AllItems = ItemMapper.Map(await _itemService.GetFilteredItemsAsync(searchString).ConfigureAwait(false));
            }
            else
            {
                offerAssignedItem.AllItems = ItemMapper.Map(await _itemService.GetItemsAsync().ConfigureAwait(false));

            }
            offerAssignedItem.OfferRefId = offerID;
            return View(offerAssignedItem); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferRefId,Name,Amount,OfferPricePer,MeasuringUnit")] OfferAssignedItem OfferAssignedItem)
        {
            if (ModelState.IsValid)
            {
                await _OfferAssignedItemService.AddAsync(OfferAssignedItemMapper.Map(OfferAssignedItem)).ConfigureAwait(false);

                return RedirectToAction("Index", new {id = offerID });
            }
            return View(OfferAssignedItem);
        }

        

        // Get : OfferAssignedItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            try
            {
                var OfferAssignedItem = await _OfferAssignedItemService.GetOfferAssignedItemAsync(id.Value).ConfigureAwait(false);
                if (OfferAssignedItem == null)
                {
                    return NotFound();
                }
                return View(OfferAssignedItemMapper.Map(OfferAssignedItem));
            }
            catch (Exception)
            {
                TempData["OfferAssignedItemCantBeFound"] = "Linjen findes ikke længere";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        // POST: OfferAssignedItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, OfferRefId,Name,Amount,OfferPricePer,MeasuringUnit,Rowversion")] OfferAssignedItem OfferAssignedItem)
        {
            try
            {
                if (id != OfferAssignedItem.Id)
                {
                    return NotFound();
                }
                var oldOfferAssignedItem = await _OfferAssignedItemService.GetOfferAssignedItemAsync(id).ConfigureAwait(false);
                if (ModelState.IsValid && OfferAssignedItemMapper.Map(oldOfferAssignedItem).Rowversion == OfferAssignedItem.Rowversion)
                {
                    await _OfferAssignedItemService.UpdateAsync(id, OfferAssignedItemMapper.Map(OfferAssignedItem)).ConfigureAwait(false);
                    return RedirectToAction("Index", new { id = offerID });
                }
                else
                {
                    TempData["OfferAssignedItemCantBeFound"] = "Linjen er blevet redigeret af en anden bruger. Prøv igen.";
                    return RedirectToAction("Index", new { id = offerID });
                }
            }
            catch (Exception)
            {

                TempData["OfferAssignedItemCantBeFound"] = "Linjen findes ikke længere.";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        //Post : OfferAssignedItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var OfferAssignedItem = await _OfferAssignedItemService.GetOfferAssignedItemAsync(id.Value).ConfigureAwait(false);
                if (OfferAssignedItem == null)
                {
                    return NotFound();
                }
                return View(OfferAssignedItemMapper.Map(OfferAssignedItem));
            }
            catch (Exception)
            {
                TempData["OfferAssignedItemCantBeFound"] = "Linjen findes ikke længere.";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        // POST: OfferAssignedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _OfferAssignedItemService.RemoveAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction("Index", new { id = offerID });
        }


        public IActionResult BackToOffer()
        {
            return RedirectToAction("Details", "Offer", new { id = offerID });
        }
    }
}