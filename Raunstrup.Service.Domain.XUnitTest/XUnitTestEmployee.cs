using Moq;
using Raunstrup.Service.Api.Controllers;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Raunstrup.Service.Domain.XUnitTest
{
    public class XUnitTestEmployee
    {
   
        [Fact]
        public void GetEmloyee()
        {
            var employeeMock = new Mock<IEmployeeService>();
            var offerMock = new Mock<IOfferService>();

            employeeMock.Setup(Employee => Employee.GetEmployee(It.IsAny<int>())).Returns(new Employee
            {
                ID = 1,
                Name = "Test Employee",
                Cpr = 310577,
                PhoneNo = 65378270,
                Email = "test@testtest.com",
                Address = "Testgade 76",
                PostalCode = 1234,
                City = "Ikke eksisterende by",
                IsProjectleader = true,
                ProfessionRefID = 1,
                Specialisation = "None",
                Username = "Tukmus"

            });

            var employeeController = new EmployeeController(employeeMock.Object, offerMock.Object);

            var test = employeeController.GetEmployee(1);

            Assert.Equal(1, test.ID);
            Assert.Equal(65378270, test.PhoneNo);
            Assert.Equal("test@testtest.com", test.Email);

        }
    }
}
