using Microsoft.AspNetCore.Mvc;
using Moq;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Raunstrup.UI.MVC.XUnitTest
{
    public class XUnitTestEmployee
    {
        private readonly Mock<IOffeEmployeeService> _mockService;
        private readonly EmployeesController _controller;

        public XUnitTestEmployee()
        {
            _mockService = new Mock<IOffeEmployeeService>();
            _controller = new EmployeesController(_mockService.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            string sortOrder = null;
            string searchString = null;
            
            var result = _controller.Index(sortOrder, searchString);

            Assert.IsType<ViewResult>(result.Result);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToIndexAction()
        {
            var employee = new Employee
            {
                Name = "Test Employee",
                Cpr = 310577,
                PhoneNo = 65378270,
                Email = "test@testtest.com",
                Address = "Testgade 76",
                PostalCode = 1234,
                City = "Ikke eksisterende by",
                IsProjectleader = true,
                ProfessionRefID = "1",
                Specialisation = "None",
                Username = "Tukmus"
            };

            var result = _controller.Create(employee);


            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
