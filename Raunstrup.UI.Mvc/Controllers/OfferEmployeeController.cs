using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;

namespace Raunstrup.UI.MVC.Controllers
{
    public class OfferEmployeeController : Controller
    {
        private readonly IOfferEmployeeService _offerEmployeeservice;
        private static int offerID;

        public OfferEmployeeController(IOfferEmployeeService offerEmployeeservice)
        {
            _offerEmployeeservice = offerEmployeeservice;
        }


        // Viser alle medabrjdere der er ansat på Raunstrup
        public async Task<IActionResult> GetNewEmployeeToOffer(int? id, string searchString)
        {
            var employeeDtos = await _offerEmployeeservice.GetEmployeesNotOnOfferAsync(offerID).ConfigureAwait(false);
            return View(EmployeeMapper.Map(employeeDtos));

        }

        // Medarbejder der er tilknyttet til projektet
        public async Task<IActionResult> GetEmployeesToOffer(int? id, string searchString)
        {
            if (id != 0)
            {
                offerID = id.Value;
            }
     
            var offeremployeeDtos = await _offerEmployeeservice.GetOfferEmployeeAsync(offerID).ConfigureAwait(false);
            return View(OfferEmployeeMapper.Map(offeremployeeDtos));

        }

        // Medarbejder der er tilknyttet til projektet
        public IActionResult GetEmployeeToOffer2()
        {
            return RedirectToAction("GetEmployeesToOffer","OfferEmployee", new { id = offerID });       

        }

        ////Knappen der tilføjer medabarjdere til projektet
        public async Task<IActionResult> AddEmployeesToOffer(int id)
        {
            OfferEmployee offerEmployee = new OfferEmployee();
            offerEmployee.EmployeeRefId = id;
            offerEmployee.OfferRefId = offerID;
            if (ModelState.IsValid)
            {
                await _offerEmployeeservice.AddEmployeeOffer(OfferEmployeeMapper.Map(offerEmployee)).ConfigureAwait(false);
            }


            return RedirectToAction("GetNewEmployeeToOffer", "OfferEmployee", new { id = offerID });
        }

        public IActionResult BacktoOffer()
        {
            return RedirectToAction("Details", "Offer", new { id = offerID });
        }

        // Sletter en medarbejder fra tilbuddet 
        public async Task<IActionResult> RemoveEmployeeFromOffer(int? id)
        {

            await _offerEmployeeservice.DeleteEmployeeAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction("GetEmployeesToOffer", "OfferEmployee", new { id = offerID });


        }

    }
}