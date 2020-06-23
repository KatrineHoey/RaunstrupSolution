using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Contract;
using Raunstrup.UI.MVC.Models.ContactModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Name, Email, Subject, Message")] ContactMV contact)
        {
            if (ModelState.IsValid)
            {
                var response = await _contactService.AddAsync(ContactMapper.Map(contact)).ConfigureAwait(false);
                ViewBag.Message = response;
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

