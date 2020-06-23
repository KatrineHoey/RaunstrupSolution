using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raunstrup.Service.Api.Models;
using Raunstrup.Service.Contract.DTO;
using Raunstrup.Service.Domain;
using Raunstrup.Service.Infrastructure.Entities;

namespace Raunstrup.Service.Api.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _EmployeeService;
        private readonly IOfferService _offerService;

        public EmployeeController(IEmployeeService employeeService, IOfferService offerService)
        {
            _EmployeeService = employeeService;
            _offerService = offerService;
        }

        // GET: api/Employee
        [HttpGet]
        public IEnumerable<EmployeeDTO> GetEmployees()
        {  
            return _EmployeeService.GetEmployees().Select(e => EmployeeMapper.Map(e));
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public EmployeeDTO GetEmployee(int id)
        {
            if (_EmployeeService.GetEmployee(id) == null)
            {
                EmployeeDTO employeeDTO = null;
                return employeeDTO;
            }
            else
            {
                return EmployeeMapper.Map(_EmployeeService.GetEmployee(id));
            }

        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public void PutEmployee(int id, [FromBody]EmployeeDTO employee)
        {
            _EmployeeService.UpdateEmployee(EmployeeMapper.Map(employee));

        }

        // POST: api/Employee
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public void PostEmployee([FromBody] EmployeeDTO employee)
        {
            _EmployeeService.CreateEmployee(EmployeeMapper.Map(employee));

        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public void DeleteEmployee(int id)
        {
            _EmployeeService.DeleteEmployee(id);
        }


        [HttpGet("search/{searchString}", Name = "GetFilteredEmployees")]
        public IEnumerable<EmployeeDTO> GetFilteredEmployees(string searchString)
        {
            return _EmployeeService.GetFilteredEmployees(searchString).Select(a => EmployeeMapper.Map(a));
        }

        [HttpPut("offer/{id}")]
        public void PutOffer(int id, [FromBody]int offerId)
        {
            _offerService.UpdateOfferProjectLeader(id, offerId);

        }


    }
}