using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;

namespace Raunstrup.UI.MVC.Controllers
{
    public class OfferUsedItemController : Controller
    {
        private readonly IOfferUsedItemService _OfferUsedItemService;
        private readonly IItemService _itemService;
        private static int offerID;

        public OfferUsedItemController(IOfferUsedItemService OfferUsedItemService, IItemService itemService)
        {
            _OfferUsedItemService = OfferUsedItemService;
            _itemService = itemService;
        }

        //Get: OfferUsedItems
        public async Task<IActionResult> Index(int id)
        {
            offerID = id;
            var offerUsedItemDtos = await _OfferUsedItemService.GetOfferUsedItemsAsync(id).ConfigureAwait(false);
            return View(OfferUsedItemMapper.Map(offerUsedItemDtos));

        }

        // Get : OfferUsedItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var OfferUsedItem = await _OfferUsedItemService.GetOfferUsedItemAsync(id.Value).ConfigureAwait(false);
                if (OfferUsedItem == null)
                {
                    return NotFound();
                }
                return View(OfferUsedItemMapper.Map(OfferUsedItem));
            }
            catch (Exception)
            {
                TempData["OfferUsedItemCantBeFound"] = "Linjen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: OfferUsedItems/Create
        public async Task<IActionResult> Create(string searchString)
        {

            // Hent alle materialer
            OfferUsedItem offerUsedItem = new OfferUsedItem();

            if (!String.IsNullOrEmpty(searchString))
            {
                offerUsedItem.AllItems = ItemMapper.Map(await _itemService.GetFilteredItemsAsync(searchString).ConfigureAwait(false));
            }
            else
            {
                offerUsedItem.AllItems = ItemMapper.Map(await _itemService.GetItemsAsync().ConfigureAwait(false));

            }
            offerUsedItem.OfferRefId = offerID;
            offerUsedItem.EmployeeRefId = GetUserId();
            return View(offerUsedItem);
        }

        protected virtual int GetUserId()
        {
            return Convert.ToInt32(User.Identity.Name);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferRefId,Name,EmployeeRefId, Amount,OfferPrice,MeasuringUnit")] OfferUsedItem OfferUsedItem)
        {
            if (ModelState.IsValid)
            {
                await _OfferUsedItemService.AddAsync(OfferUsedItemMapper.Map(OfferUsedItem)).ConfigureAwait(false);

                return RedirectToAction("Index", new { id = offerID });
            }
            return View(OfferUsedItem);
        }



        // Get : OfferUsedItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            try
            {
                var OfferUsedItem = await _OfferUsedItemService.GetOfferUsedItemAsync(id.Value).ConfigureAwait(false);
                if (OfferUsedItem == null)
                {
                    return NotFound();
                }
                return View(OfferUsedItemMapper.Map(OfferUsedItem));
            }
            catch (Exception)
            {
                TempData["OfferUsedItemCantBeFound"] = "Linjen findes ikke længere";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        // POST: OfferUsedItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, OfferRefId,Name,EmployeeRefId, ItemRefId, Amount,OfferPrice,MeasuringUnit,Rowversion")] OfferUsedItem OfferUsedItem)
        {
            try
            {
                if (id != OfferUsedItem.Id)
                {
                    return NotFound();
                }
                var oldOfferUsedItem = await _OfferUsedItemService.GetOfferUsedItemAsync(id).ConfigureAwait(false);
                if (ModelState.IsValid && OfferUsedItemMapper.Map(oldOfferUsedItem).Rowversion == OfferUsedItem.Rowversion)
                {
                    await _OfferUsedItemService.UpdateAsync(id, OfferUsedItemMapper.Map(OfferUsedItem)).ConfigureAwait(false);
                    return RedirectToAction("Index", new { id = offerID });
                }
                else
                {
                    TempData["OfferUsedItemCantBeFound"] = "Linjen er blevet redigeret af en anden bruger. Prøv igen.";
                    return RedirectToAction("Index", new { id = offerID });
                }
            }
            catch (Exception)
            {

                TempData["OfferUsedItemCantBeFound"] = "Linjen findes ikke længere.";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        //Post : OfferUsedItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var OfferUsedItem = await _OfferUsedItemService.GetOfferUsedItemAsync(id.Value).ConfigureAwait(false);
                if (OfferUsedItem == null)
                {
                    return NotFound();
                }
                return View(OfferUsedItemMapper.Map(OfferUsedItem));
            }
            catch (Exception)
            {
                TempData["OfferUsedItemCantBeFound"] = "Linjen findes ikke længere.";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        // POST: OfferUsedItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _OfferUsedItemService.RemoveAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction("Index", new { id = offerID });
        }


        public IActionResult BackToOffer()
        {
            return RedirectToAction("Details", "Offer", new { id = offerID });
        }
    }
}