using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using System.Resources;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Raunstrup.UI.MVC.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;
        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        //Get: Offers
        public async Task<IActionResult> Index()
        {
            var offerDtos = await _offerService.GetOffersAsync().ConfigureAwait(false);
            return View(OfferMapper.Map(offerDtos));
        }

        // Get : Offers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var offer = await _offerService.GetOfferAsync(id.Value).ConfigureAwait(false);
                if (offer == null)
                {
                    return NotFound();
                }
                return View(OfferMapper.Map(offer));
            }
            catch (Exception)
            {
                TempData["OfferCantBeFound"] = "Tilbuddet findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Offers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkingTitle,StartDate,EndDate,Description, PayForUsedItems,IsAccepted,IsDone,IsActive,Rowversion")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                await _offerService.AddAsync(OfferMapper.Map(offer)).ConfigureAwait(false);

                return RedirectToAction(nameof(Index));
            }

            return View(offer);
        }

        // Get : Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
 
            try
            {
                var offer = await _offerService.GetOfferAsync(id.Value).ConfigureAwait(false);
                if (offer == null)
                {
                    return NotFound();
                }
                return View(OfferMapper.Map(offer));
            }
            catch (Exception)
            {
                TempData["OfferCantBeFound"] = "Tilbuddet findes ikke længere";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Offers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkingTitle,StartDate,EndDate,Description, PayForUsedItems, IsAccepted,IsDone," +
            "IsActive,Rowversion, ProjectleaderRefId, TotalPrice, DiscountProcent, TotalPriceWithDiscount, CustomerId, AssignedItems, UsedItems" +
            "WorkingHours, ProjectDrivings, ProjectEmployees")] Offer offer)
        {
            try
            {
                if (id != offer.Id)
                {
                    return NotFound();
                }
                var oldOffer = await _offerService.GetOfferAsync(id).ConfigureAwait(false);
                if (ModelState.IsValid && OfferMapper.Map(oldOffer).Rowversion == offer.Rowversion)
                {
                    await _offerService.UpdateAsync(id, OfferMapper.Map(offer)).ConfigureAwait(false);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["OfferCantBeFound"] = "Tilbuddet er blevet redigeret af en anden bruger. Prøv igen.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                TempData["OfferCantBeFound"] = "Tilbuddet findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        //Post : Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var offer = await _offerService.GetOfferAsync(id.Value).ConfigureAwait(false);
                if (offer == null)
                {
                    return NotFound();
                }
                return View(OfferMapper.Map(offer));
            }
            catch (Exception)
            {
                TempData["OfferCantBeFound"] = "Tilbuddet findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _offerService.RemoveAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> PrintAssignedOfferToPdf(int? id)
        {
            var offer = await _offerService.GetOfferAsync(id.Value).ConfigureAwait(false);
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);

            tw.WriteLine("Titel: " + offer.WorkingTitle);
            tw.WriteLine("Startdato: " + offer.StartDate);
            tw.WriteLine("Slutdato: " + offer.EndDate);
            if (offer.Customer != null)
            {
                tw.WriteLine("Kundens navn: " + offer.Customer.Name);
                tw.WriteLine("Kundes tlf.: " + offer.Customer.PhoneNo);
            }
            if(offer.Projectleader != null)
            {
                tw.WriteLine("Projetleder: " + offer.Projectleader.Name);
                tw.WriteLine("Projetleders tlf.: " + offer.Projectleader.PhoneNo);
            }
          
            tw.WriteLine();

            if (offer.AssignedItems.Count() > 0)
            {
                tw.WriteLine("Beskrivelse \t Antal \t Pris \t Måleenhed");
                decimal sum = 0;
                foreach (var item in offer.AssignedItems)
                {
                    sum = sum + (item.Amount * item.OfferPricePer);
                    if (item.Name.Length <= 3)
                    {
                        tw.WriteLine($"{item.Name} \t\t {item.Amount} \t {item.OfferPricePer} \t {item.MeasuringUnit}");
                    }
                    else
                    {
                        tw.WriteLine($"{item.Name} \t {item.Amount} \t {item.OfferPricePer} \t {item.MeasuringUnit}");
                    }
                }
                tw.WriteLine();
                tw.WriteLine("Total: " + sum.ToString());
            }

            tw.Flush();

            var length = memoryStream.Length;
            tw.Close();
            var toWrite = new byte[length];
            Array.Copy(memoryStream.GetBuffer(), 0, toWrite, 0, length);

            string filename = offer.WorkingTitle + ".txt";
            return File(toWrite, "text/plain", filename);

        }

        public async Task<IActionResult> PrintUsedOfferToPdf(int? id)
        {
            var offer = await _offerService.GetOfferAsync(id.Value).ConfigureAwait(false);
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);

            tw.WriteLine("Titel: " + offer.WorkingTitle);
            tw.WriteLine("Startdato: " + offer.StartDate);
            tw.WriteLine("Slutdato: " + offer.EndDate);
            if (offer.Customer != null)
            {
                tw.WriteLine("Kundens navn: " + offer.Customer.Name);
                tw.WriteLine("Kundes tlf.: " + offer.Customer.PhoneNo);
            }
            if (offer.Projectleader != null)
            {
                tw.WriteLine("Projetleder: " + offer.Projectleader.Name);
                tw.WriteLine("Projetleders tlf.: " + offer.Projectleader.PhoneNo);
            }

            tw.WriteLine();

            decimal sumItems = 0;
            if (offer.UsedItems.Count() > 0)
            {
                tw.WriteLine("Materiale \t Beskrivelse \t Antal \t Pris \t Måleenhed");
               
                foreach (var item in offer.UsedItems)
                {
                    sumItems = sumItems + (item.Amount * item.OfferPrice);
                    if (item.Name.Length <= 3 || item.Item.ItemName.Length <= 3)
                    {
                        tw.WriteLine($"{item.Item.ItemName} \t {item.Name} \t\t {item.Amount} \t {item.OfferPrice} \t {item.MeasuringUnit}");
                    }
                    else
                    {
                        tw.WriteLine($"{item.Item.ItemName} \t {item.Name} \t {item.Amount} \t {item.OfferPrice} \t {item.MeasuringUnit}");
                    }
                }
                tw.WriteLine();
                tw.WriteLine($"Total for materialer: {sumItems} kr. ");
            }

            decimal sumHours = 0;
            if (offer.WorkingHours.Count() > 0)
            {
                
                int count = 0;
                foreach (var item in offer.WorkingHours)
                {
                    sumHours = sumHours + (item.Amount * item.HourlyPrice);
                    count = count + item.Amount;  
                }
                tw.WriteLine();
                tw.WriteLine($"Antal arbejdstimer: {count} Total for arbejdstimer: {sumHours} kr.");
            }

            decimal sumDriving = 0;
            if (offer.ProjectDrivings.Count() > 0)
            {
                
                foreach (var item in offer.ProjectDrivings)
                {
                    sumDriving = sumDriving + (item.Amount * item.Price);
                }
                tw.WriteLine();
                tw.WriteLine($"Total for kørsel: {sumDriving} kr.");
            }

            tw.WriteLine($"Total i alt: {sumItems + sumHours + sumDriving} kr.");
            tw.Flush();

            var length = memoryStream.Length;
            tw.Close();
            var toWrite = new byte[length];
            Array.Copy(memoryStream.GetBuffer(), 0, toWrite, 0, length);

            string filename = offer.WorkingTitle + ".txt";
            return File(toWrite, "text/plain", filename);

        }


        public async Task<IActionResult> AddDiscountToOffer(int id)
        {
            await _offerService.AddDiscount(id).ConfigureAwait(false);
            return RedirectToAction("Details", "Offer", new { id = id });
        }

    }
}