using System;
using System.Collections.Generic;
using System.Text;

namespace Raunstrup.Service.Contract.DTO
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public int Cpr { get; set; }
        public string Name { get; set; }
        public int PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public bool IsProjectleader { get; set; }
        public int ProfessionRefID { get; set; }
        public ProfessionDTO Profession { get; set; }
        public string Specialisation { get; set; }
        public string Username { get; set; } 
        public int RowVersion { get; set; }
    }
}
