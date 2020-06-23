using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Raunstrup.Service.Contract.DTO;


namespace Raunstrup.Service.Contract.Services
{
    public interface IOffeEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeAsync(int id);
        Task AddAsync(EmployeeDTO employee);
        Task UpdateAsync(int id, EmployeeDTO employee);
        Task RemoveAsync(int id);
        Task<IEnumerable<EmployeeDTO>> GetFilteredEmployeesAsync(string searchString);
        Task UpdateAsync(int id, int employeeId);


    }
}
