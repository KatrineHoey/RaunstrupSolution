using System;
using System.Collections.Generic;
using System.Text;
using Raunstrup.Service.Infrastructure.Entities;

namespace Raunstrup.Service.Domain
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int id);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
        void GetProjectLeaderToOffer(Employee employee);
        IEnumerable<Employee> GetFilteredEmployees(string searchString);
    }
}
