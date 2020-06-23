using System;
using System.Collections.Generic;
using System.Text;
using Raunstrup.Service.Infrastructure.Entities;
using Raunstrup.Service.Infrastructure.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Raunstrup.Service.Domain
{
    public class DomainEmployeeService : IEmployeeService
    {
        private readonly RaunstrupContext _context;

        public DomainEmployeeService(RaunstrupContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            foreach (Employee e in _context.Employees.Include(x => x.Profession).ToList())
            {
                if (e.Cpr != 0)
                {
                    employees.Add(e);
                }
            }
            return employees;
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees
                .Include(x => x.Profession)
                .FirstOrDefault(x => x.ID == id); ;
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Employees.Any(c => c.ID == employee.ID)) //Tjekker at employeen stadig findes i databasen. 
            {
                employee.RowVersion = employee.RowVersion + 1; //Viser at der er blevet foretaget en ændring ved denne employee. 
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
        }
        public void CreateEmployee(Employee employee)
        {

            employee.RowVersion = 1;
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //Hvis der trackes, så sker der fejl.
            if (_context.Employees.Any(c => c.ID == id)) //Tjekker at employeen stadig findes i databasen. 
            {
                Employee employee = _context.Employees.Find(id);
                employee.Cpr = 0000000000;
                employee.Email = "ukendt@ukendt.dk";
                employee.Address = "Ukendt";
                employee.City = "Ukendt";
                employee.PostalCode = 0000;
                employee.Username = "Ugyldig";
                employee.RowVersion = employee.RowVersion + 1; //Viser at der er blevet foretaget en ændring ved denne employee. 
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }

        }


        public IEnumerable<Employee> GetFilteredEmployees(string searchString)
        {

            return _context.Employees
                .Where(f => f.Name.ToUpper().Contains(searchString.ToUpper())
            || f.ID.ToString().Contains(searchString)
            || f.PhoneNo.ToString().Contains(searchString)
            || f.Specialisation.ToUpper().Contains(searchString.ToUpper()))
            .Include(x => x.Profession);

        }


        public void GetProjectLeaderToOffer(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void AddEmployeesToOffer(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }




        //    Employee til offer
        //    public IEnumerable<Employee> GetOfferEmployees(int offerid)
        //    {
        //        List<Employee> offeremployees = new List<Employee>();
        //        System.Collections.IList list = _context.OfferEmployees.Include(x => x.OfferRefId == offerid).ToList();
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            Employee e = (Employee)list[i];
        //            if (e. != 0)
        //            {
        //                employees.Add(e);
        //            }
        //        }
        //        foreach (Employee e in _context.OfferEmployees.Include(x => x.OfferRefId == offerid).ToList())
        //        {
        //            if (e. != 0)
        //            {
        //                employees.Add(e);
        //            }
        //        }
        //        return employees;
        //        //return _context.OfferEmployees.Where(x => x.OfferRefId == offerid).ToList();
        //        //return (IEnumerable<Employee>)_context.OfferEmployees.Where(x => x.OfferRefId == offerid).ToList();
        //    }
        //}
    }
}
