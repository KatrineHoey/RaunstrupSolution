using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;

namespace Raunstrup.UI.MVC.Controllers
{
    public class OfferWorkingHoursController : Controller
    {
        private readonly IOfferWorkingHoursService _OfferWorkingHoursService;

        private static int offerID;
        public OfferWorkingHoursController(IOfferWorkingHoursService offerWorkingHoursService)
        {
            _OfferWorkingHoursService = offerWorkingHoursService;
        }

        //Get: OfferWorkingHourss
        public async Task<IActionResult> Index(int id)
        {
            offerID = id;
            var offerWorkingHoursDtos = await _OfferWorkingHoursService.GetOfferWorkingHoursAsync(id).ConfigureAwait(false);
            if (IsInRole("User"))
            {
                offerWorkingHoursDtos = offerWorkingHoursDtos.Where(x => x.EmployeeRefId == GetUserId());
            }

            return View(OfferWorkingHoursMapper.Map(offerWorkingHoursDtos));

        }

        protected virtual  bool IsInRole(string role)
        {
            return User.IsInRole(role);
        }

        protected virtual int GetUserId()
        {
            return Convert.ToInt32(User.Identity.Name);
        }


        // GET: OfferWorkingHours/Create
        public IActionResult Create()
        {
            OfferWorkingHours offerWorkingHours = new OfferWorkingHours();
            offerWorkingHours.OfferRefId = offerID;
            offerWorkingHours.DateOfWorking = DateTime.Now;
            offerWorkingHours.EmployeeRefId = GetUserId();
            return View(offerWorkingHours);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferRefId,EmployeeRefId,DateOfWorking,Amount, Rowversion")] OfferWorkingHours offerWorkingHours)
        {
            if (ModelState.IsValid)
            {
                await _OfferWorkingHoursService.AddAsync(OfferWorkingHoursMapper.Map(offerWorkingHours)).ConfigureAwait(false);

                return RedirectToAction("Index", new { id = offerID });
            }
            return View(offerWorkingHours);
        }



        // Get : OfferWorkingHourss/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            try
            {
                var offerWorkingHours = await _OfferWorkingHoursService.GetOfferWorkingHourAsync(id.Value).ConfigureAwait(false);
                if (offerWorkingHours == null)
                {
                    return NotFound();
                }
                return View(OfferWorkingHoursMapper.Map(offerWorkingHours));
            }
            catch (Exception)
            {
                TempData["OfferWorkingHoursCantBeFound"] = "Linjen findes ikke længere";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        // POST: OfferWorkingHourss/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, OfferRefId,EmployeeRefId,DateOfWorking,Amount, Rowversion")] OfferWorkingHours offerWorkingHours)
        {
            try
            {
                if (id != offerWorkingHours.Id)
                {
                    return NotFound();
                }
                var oldOfferWorkingHours = await _OfferWorkingHoursService.GetOfferWorkingHourAsync(id).ConfigureAwait(false);
                if (ModelState.IsValid && OfferWorkingHoursMapper.Map(oldOfferWorkingHours).Rowversion == offerWorkingHours.Rowversion)
                {
                    await _OfferWorkingHoursService.UpdateAsync(id, OfferWorkingHoursMapper.Map(offerWorkingHours)).ConfigureAwait(false);
                    return RedirectToAction("Index", new { id = offerID });
                }
                else
                {
                    TempData["OfferWorkingHoursCantBeFound"] = "Linjen er blevet redigeret af en anden bruger. Prøv igen.";
                    return RedirectToAction("Index", new { id = offerID });
                }
            }
            catch (Exception)
            {

                TempData["OfferWorkingHoursCantBeFound"] = "Linjen findes ikke længere.";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        //Post : OfferWorkingHourss/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var offerWorkingHours = await _OfferWorkingHoursService.GetOfferWorkingHourAsync(id.Value).ConfigureAwait(false);
                if (offerWorkingHours == null)
                {
                    return NotFound();
                }
                return View(OfferWorkingHoursMapper.Map(offerWorkingHours));
            }
            catch (Exception)
            {
                TempData["OfferWorkingHoursCantBeFound"] = "Linjen findes ikke længere.";
                return RedirectToAction("Index", new { id = offerID });
            }
        }

        // POST: OfferWorkingHourss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _OfferWorkingHoursService.RemoveAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction("Index", new { id = offerID });
        }


        public IActionResult BackToOffer()
        {
            return RedirectToAction("Details", "Offer", new { id = offerID });
        }
    }
}