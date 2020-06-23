using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;

namespace Raunstrup.UI.MVC.Controllers
{
    public class OfferDrivingController : Controller
    {
        private readonly IOfferDrivingService _offerDrivingService;
        private static int offerID;

        public OfferDrivingController(IOfferDrivingService offerDrivingService)
        {
            _offerDrivingService = offerDrivingService;
        }

        // GET: OfferDrivings
        public async Task<IActionResult> Index(int id)
        {
            offerID = id;
            var offerDrivingDtos = await _offerDrivingService.GetOfferDrivingsAsync(id).ConfigureAwait(false);
            return View(OfferDrivingMapper.Map(offerDrivingDtos));
        }

        // GET: OfferDriving/Create
        public IActionResult Create()
        {
            OfferDriving offerDriving = new OfferDriving();
            offerDriving.OfferRefId = offerID;
            offerDriving.TodaysDate = DateTime.Today;
            offerDriving.Price = "4";
            offerDriving.EmployeeRefId = GetUserId();
            return View(offerDriving);
        }

        protected virtual int GetUserId()
        {
            return Convert.ToInt32(User.Identity.Name);
        }

        // GET : OfferDriving/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferRefId, EmployeeRefId, TodaysDate, Price, Amount, Rowversion")] OfferDriving offerDriving)
        {

            if (ModelState.IsValid)
            {
                await _offerDrivingService.AddAsync(OfferDrivingMapper.Map(offerDriving)).ConfigureAwait(false);
                return RedirectToAction("Index", new { id = offerID });
            }
            return View(offerDriving);
        }

        // GET : OfferDriving/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var offerDriving = await _offerDrivingService.GetOfferDrivingAsync(id.Value).ConfigureAwait(false);
                if (offerDriving == null)
                {
                    return NotFound();
                }
                return View(OfferDrivingMapper.Map(offerDriving));
            }
            catch (Exception)
            {

                TempData["OfferDrivingCantBeFound"] = "Kørselen findes ikke";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        // POST : OfferDriving/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, OfferRefId, EmployeeRefId, TodaysDate, Price, Amount, Rowversion")] OfferDriving offerDriving)
        {
            try
            {
                if (id != offerDriving.Id)
                {
                    return NotFound();
                }
                var oldOfferDriving = await _offerDrivingService.GetOfferDrivingAsync(id).ConfigureAwait(false);
                if (ModelState.IsValid && OfferDrivingMapper.Map(oldOfferDriving).Rowversion == offerDriving.Rowversion)
                {
                    await _offerDrivingService.UpdateAsync(id, OfferDrivingMapper.Map(offerDriving)).ConfigureAwait(false);
                    return RedirectToAction("Index", new { id = offerID });
                }
                else
                {
                    TempData["OfferDrivingCantBeFound"] = "Kørselen er blevet af en anden bruger. prøve igen";
                    return RedirectToAction("Index", new { id = offerID });
                }
            }
            catch (Exception)
            {

                TempData["OfferDrivingCantBeFound"] = "Kørselen findes ikke længere";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        // POST : OfferDriving/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var offerDriving = await _offerDrivingService.GetOfferDrivingAsync(id.Value).ConfigureAwait(false);
                if (offerDriving == null)
                {
                    return NotFound();
                }
                return View(OfferDrivingMapper.Map(offerDriving));
            }
            catch (Exception)
            {
                TempData["OfferDrivingCantBeFound"] = "Kørselen findes ikke længere";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        // POST : OfferDriving/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _offerDrivingService.RemoveAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction("Index", new { id = offerID });
        }

        public IActionResult BacktoOffer()
        {
            return RedirectToAction("Details", "Offer", new { id = offerID });
        }

    }
}