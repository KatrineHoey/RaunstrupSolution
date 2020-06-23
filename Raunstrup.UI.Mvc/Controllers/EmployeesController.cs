using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC
{
    public class EmployeesController : Controller
    {
        private readonly IOffeEmployeeService _employeeservice;
        private static int offerID;

        public EmployeesController(IOffeEmployeeService employeeservice)
        {
            _employeeservice = employeeservice;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewData["Name2SortParm"] = sortOrder == "name" ? "name_desc" : "id";
       //     ViewData["CurrentFilter"] = searchString;

            // Hent data
            var employeeDtos = await _employeeservice.GetEmployeesAsync().ConfigureAwait(false);

            var employees = from e in employeeDtos
                            select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = await _employeeservice.GetFilteredEmployeesAsync(searchString).ConfigureAwait(false);
            }
            switch (sortOrder)
            {
                case "name":
                    employees = employees.OrderBy(e => e.Name);
                    break;
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.Name);
                    break;
                case "id":
                    employees = employees.OrderBy(e => e.ID);
                    break;
                default:
                    employees = employees.OrderBy(e => e.Name);
                    break;
            }

            return View(EmployeeMapper.Map(employees));
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var employee = await _employeeservice.GetEmployeeAsync(id.Value).ConfigureAwait(false);

                if (employee == null) return NotFound();

                return View(EmployeeMapper.Map(employee));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["EmployeeCantBeFound"] = "Medarbejderen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var model = new Employee();
            model.ProfessionRefID = "1";
            return View(model);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpr,Name,PhoneNo,Email,Address ,PostalCode,City,IsProjectleader,ProfessionRefID,Specialisation,Username,RowVersion")] Employee employee)
        {

            if (ModelState.IsValid)
            {
                await _employeeservice.AddAsync(EmployeeMapper.Map(employee)).ConfigureAwait(false);

                return RedirectToAction(nameof(Index));
            }


            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            try
            {
                var employee = await _employeeservice.GetEmployeeAsync(id.Value).ConfigureAwait(false);
                if (employee == null) return NotFound();

                return View(EmployeeMapper.Map(employee));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["EmployeeCantBeFound"] = "Medarbejderen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Cpr,Name,PhoneNo,Email,Address ,PostalCode,City,IsProjectleader,ProfessionRefID,Specialisation,Username,RowVersion")] Employee employee)
        {
            try
            {
                if (id != employee.ID) return NotFound();
                var oldEmployee = await _employeeservice.GetEmployeeAsync(id).ConfigureAwait(false); //Henter den nyeste opdateret vare fra databasen.

                if (ModelState.IsValid && EmployeeMapper.Map(oldEmployee).RowVersion == employee.RowVersion) //Der sammenlignes om den nyeste vare og den nuværende vare har samme rowversion.
                { //Hvis rowversion er ens, så der ikke andre brugere som har været inde og ændre i varen. 

                    await _employeeservice.UpdateAsync(id, EmployeeMapper.Map(employee)).ConfigureAwait(false);

                    return RedirectToAction(nameof(Index));
                }
                else
                {//Laver en fejlmeddelse 
                    TempData["EmployeeCantBeFound"] = "Medarbejderen er blevet redigeret af en anden bruger. Prøv igen.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                //Laver fejlmeddelse
                TempData["EmployeeCantBeFound"] = "Medarbejderen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            try
            {
                var employee = await _employeeservice.GetEmployeeAsync(id.Value).ConfigureAwait(false);
                if (employee == null) return NotFound();
                return View(EmployeeMapper.Map(employee));
            }
            catch (Exception)
            { //Laver fejlmeddelse
                TempData["EmployeeCantBeFound"] = "Medarbejderen findes ikke længere.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _employeeservice.RemoveAsync(id.Value).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        //private bool EmployeeExists(int id)
        //{
        //    return _employeeservice.Employees.Any(e => e.ID == id);
        //}

        public async Task<IActionResult> GetProjectLeaderToOffer(int id, string searchString)
        {
            offerID = id;
            if (!String.IsNullOrEmpty(searchString))
            {
                var employeeDtos = await _employeeservice.GetFilteredEmployeesAsync(searchString).ConfigureAwait(false);
                return View(EmployeeMapper.Map(employeeDtos));
            }
            else
            {
                var employeeDtos = await _employeeservice.GetEmployeesAsync().ConfigureAwait(false);
                return View(EmployeeMapper.Map(employeeDtos));
            }
        }

        public async Task<IActionResult> GetOfferProjectLeader(int id)
        {
            if (ModelState.IsValid)
            {

                await _employeeservice.UpdateAsync(id, offerID).ConfigureAwait(false);
            }
            return RedirectToAction("Details", "Offer", new { id = offerID });
        }
        public IActionResult BackToOffer()
        {
            return RedirectToAction("Details", "Offer", new { id = offerID });
        }


    }
}
