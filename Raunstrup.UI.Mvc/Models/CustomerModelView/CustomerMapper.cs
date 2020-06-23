using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models
{
    public class CustomerMapper
    {
        public static Customer Map(CustomerDTO dto)
        {
            if (dto == null)
            {
                return null;
            }
            return new Customer
            {ID= dto.ID, Name = dto.Name, Address = dto.Address, City = dto.City, Email = dto.Email, PhoneNo = dto.PhoneNo, DiscountGroup = dto.DiscountGroup.ToString(), RowVersion = dto.RowVersion };
        }

        public static IEnumerable<Customer> Map(IEnumerable<CustomerDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static CustomerDTO Map(Customer view)
        {
            if (view == null)
            {
                return null;
            }

            return new CustomerDTO
            {ID = view.ID, Name = view.Name, Address = view.Address, City = view.City, Email = view.Email, PhoneNo = view.PhoneNo, DiscountGroup = Convert.ToInt32(view.DiscountGroup), RowVersion = view.RowVersion};
        }
    }
}
