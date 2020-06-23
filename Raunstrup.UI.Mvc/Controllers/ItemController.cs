using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Data;
using Raunstrup.UI.MVC.Models;

namespace Raunstrup.UI.MVC.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemservice;

        public ItemsController(IItemService itemservice)
        {
            _itemservice = itemservice;
        }

        // GET: Items
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewData["Name2SortParm"] = sortOrder == "name" ? "name_desc" : "id";
     //       ViewData["CurrentFilter"] = searchString;

            // Hent data
            var itemsDtos = await _itemservice.GetItemsAsync().ConfigureAwait(false);

            var items = from i in itemsDtos
                        select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = await _itemservice.GetFilteredItemsAsync(searchString).ConfigureAwait(false);
            }
            switch (sortOrder)
            {
                case "name":
                    items = items.OrderByDescending(i => i.ItemName);
                    break;
                case "name_desc":
                    items = items.OrderBy(i => i.SalePrice);
                    break;
                case "id":
                    items = items.OrderBy(i => i.PurchasePrice);
                    break;
                default:
                    items = items.OrderBy(i => i.ItemName);
                    break;
            }

            return View(ItemMapper.Map(items));
            
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var item = await _itemservice.GetItemAsync(id.Value).ConfigureAwait(false);

                if (item == null) return NotFound();

                return View(ItemMapper.Map(item));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["ItemCantBeFound"] = "Varen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemNo,ItemName,PurchasePrice,SalePrice,MeasuringUnit, RowVersion")] Item item)
        {
            if (ModelState.IsValid)
            {
                await _itemservice.AddAsync(ItemMapper.Map(item)).ConfigureAwait(false);

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }



        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
 
            try
            {
                var item = await _itemservice.GetItemAsync(id.Value).ConfigureAwait(false);
                if (item == null) return NotFound();

                return View(ItemMapper.Map(item));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["ItemCantBeFound"] = "Varen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, ItemNo, ItemName,PurchasePrice,SalePrice,MeasuringUnit,Active, RowVersion")] Item item)
        {
            try
            {
                if (id != item.ID) return NotFound();
                var oldItem = await _itemservice.GetItemAsync(id).ConfigureAwait(false); //Henter den nyeste opdateret vare fra databasen.

                if (ModelState.IsValid && ItemMapper.Map(oldItem).RowVersion == item.RowVersion) //Der sammenlignes om den nyeste vare og den nuværende vare har samme rowversion.
                { //Hvis rowversion er ens, så der ikke andre brugere som har været inde og ændre i varen. 

                    await _itemservice.UpdateAsync(id, ItemMapper.Map(item)).ConfigureAwait(false);

                    return RedirectToAction(nameof(Index));
                }
                else
                {//Laver en fejlmeddelse 
                    TempData["ItemCantBeFound"] = "Varen er blevet redigeret af en anden bruger. Prøv igen.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                //Laver fejlmeddelse
                TempData["ItemCantBeFound"] = "Varen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var item = await _itemservice.GetItemAsync(id.Value).ConfigureAwait(false);
                if (item == null) return NotFound();
                return View(ItemMapper.Map(item));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["ItemCantBeFound"] = "Varen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _itemservice.RemoveAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        
    }
}