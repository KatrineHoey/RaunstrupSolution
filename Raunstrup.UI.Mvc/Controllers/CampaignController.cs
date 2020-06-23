using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;

namespace Raunstrup.UI.MVC.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        // GET : Campaign/all
        public async Task<IActionResult> Index()
        {
            var CampaignDtos = await _campaignService.GetAllCampaignsAsync().ConfigureAwait(false);
            return View(CampaignMapper.Map(CampaignDtos));
        }

        // GET : campaign/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var campaign = await _campaignService.GetCampaign(id.Value).ConfigureAwait(false);
                if (campaign == null) return NotFound();
                return View(CampaignMapper.Map(campaign));
            }
            catch (Exception)
            {
                //Laver fejlmeddelse
                TempData["CampaignCantBeFound"] = "Kampagne findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET : Campaign/Create 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campaign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(" Title, Procent, StartDate, EndDate, Rowversion,")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                await _campaignService.AddAsync(CampaignMapper.Map(campaign)).ConfigureAwait(false);

                return RedirectToAction(nameof(Index));
            }
            return View(campaign);
        }

        // GET : campaign/Details/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var campaign = await _campaignService.GetCampaign(id.Value).ConfigureAwait(false);
                if (campaign == null) return NotFound();

                return View(CampaignMapper.Map(campaign));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["CampaignCantBeFound"] = "Kampgne findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Campaign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CampaignId, Title, Procent, StartDate, EndDate, Rowversion,")] Campaign campaign)
        {
            try
            {
                if (id != campaign.CampaignId) return NotFound();
                var oldCampaign = await _campaignService.GetCampaign(id).ConfigureAwait(false); //Henter den nyeste opdateret vare fra databasen.

                if (ModelState.IsValid && CampaignMapper.Map(campaign).Rowversion == campaign.Rowversion) //Der sammenlignes om den nyeste vare og den nuværende vare har samme rowversion.
                { //Hvis rowversion er ens, så der ikke andre brugere som har været inde og ændre i varen. 

                    await _campaignService.UpdateAsync(CampaignMapper.Map(campaign), id).ConfigureAwait(false);

                    return RedirectToAction(nameof(Index));
                }
                else
                {//Laver en fejlmeddelse 
                    TempData["CampaignCantBeFound"] = "Kampange er blevet redigeret af en anden bruger. Prøv igen.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                //Laver fejlmeddelse
                TempData["CampaignCantBeFound"] = "Kampange findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Campaign/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var campaign = await _campaignService.GetCampaign(id.Value).ConfigureAwait(false);
                if (campaign == null) return NotFound();
                return View(CampaignMapper.Map(campaign));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["CampaignCantBeFound"] = "Kampange findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Campaign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _campaignService.RemoveAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }
    }
}
