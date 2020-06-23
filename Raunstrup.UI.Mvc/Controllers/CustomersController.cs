using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Data;
using Raunstrup.UI.MVC.Models;

namespace Raunstrup.UI.MVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        private static int offerID;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewData["Name2SortParm"] = sortOrder == "name" ? "email" : "id";
      //      ViewData["CurrentFilter"] = searchString;

            // Hent data
            var customerDtos = await _customerService.GetCustomersAsync().ConfigureAwait(false);

            var customers = from c in customerDtos
                            select c;


            if (!String.IsNullOrEmpty(searchString))
            {
                customers = await _customerService.GetFilteredCustomersAsync(searchString).ConfigureAwait(false);
             //   return View(CustomerMapper.Map(customerDtos));
            }
            switch (sortOrder)
            {
                case "name":
                    customers = customers.OrderByDescending(c => c.Name);
                    break;
                case "email":
                    customers = customers.OrderByDescending(c => c.Email);
                    break;
                case "id":
                    customers = customers.OrderBy(c => c.ID);
                    break;
                default:
                    customers = customers.OrderBy(c => c.Name);
                    break;
            }

            return View(CustomerMapper.Map(customers));

        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var customer = await _customerService.GetCustomerAsync(id.Value).ConfigureAwait(false);

                if (customer == null) return NotFound();

                return View(CustomerMapper.Map(customer));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["CustomerCantBeFound"] = "Kunden findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            var model = new Customer();
            model.DiscountGroup = "5";
            return View(model);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,PhoneNo,Email,Address,City,DiscountGroup")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddAsync(CustomerMapper.Map(customer)).ConfigureAwait(false);

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            try
            {
                var customer = await _customerService.GetCustomerAsync(id.Value).ConfigureAwait(false);
                if (customer == null) return NotFound();

                return View(CustomerMapper.Map(customer));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["CustomerCantBeFound"] = "Kunden findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,PhoneNo,Email,Address,City,DiscountGroup, RowVersion")] Customer customer)
        {
            try
            {
                if (id != customer.ID) return NotFound();
                var oldCustomer = await _customerService.GetCustomerAsync(id).ConfigureAwait(false); //Henter den nyeste opdateret kunde fra databasen.

                if (ModelState.IsValid && CustomerMapper.Map(oldCustomer).RowVersion == customer.RowVersion) //Der sammenlignes om den nyeste kunde og den nuværende kunde har samme rowversion.
                { //Hvis rowversion er ens, så der ikke andre brugere som har været inde og ændre i kunden. 

                    await _customerService.UpdateAsync(id, CustomerMapper.Map(customer)).ConfigureAwait(false);

                    return RedirectToAction(nameof(Index));
                }
                else
                {//Laver en fejlmeddelse 
                    TempData["CustomerCantBeFound"] = "Kunden er blevet redigeret af en anden bruger. Prøv igen.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                //Laver fejlmeddelse
                TempData["CustomerCantBeFound"] = "Kunden findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var customer = await _customerService.GetCustomerAsync(id.Value).ConfigureAwait(false);
                if (customer == null) return NotFound();
                return View(CustomerMapper.Map(customer));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["CustomerCantBeFound"] = "Kunden findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _customerService.RemoveAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        //private bool CustomerExists(int id)
        //{
        //    return _context.Customers.Any(e => e.ID == id);
        //}

        public async Task<IActionResult> GetOfferCustomer(int id, string searchString)
        {
            offerID = id;
            if (!String.IsNullOrEmpty(searchString))
            {
                var customerDtos = await _customerService.GetFilteredCustomersAsync(searchString).ConfigureAwait(false);
                return View(CustomerMapper.Map(customerDtos));
            }
            else
            {
                var customerDtos = await _customerService.GetCustomersAsync().ConfigureAwait(false);
                return View(CustomerMapper.Map(customerDtos));
            }
        }

        public async Task<IActionResult> GetCustomerToOffer(int id)
        {
            if (ModelState.IsValid)
            {

                await _customerService.UpdateAsync(id, offerID).ConfigureAwait(false);
            }
            return RedirectToAction("Details", "Offer", new { id = offerID });
        }

        public IActionResult BackToOffer()
        {
            return RedirectToAction("Details", "Offer", new { id = offerID });
        }
    }
}
