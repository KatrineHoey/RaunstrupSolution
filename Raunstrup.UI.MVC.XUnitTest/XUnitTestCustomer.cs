using Microsoft.AspNetCore.Mvc;
using Moq;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Controllers;
using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Raunstrup.UI.MVC.XUnitTest
{
    public class XUnitTestCustomer
    {
        private readonly Mock<ICustomerService> _mockService;
        private readonly CustomersController _controller;

        public XUnitTestCustomer()
        {
            _mockService = new Mock<ICustomerService>();
            _controller = new CustomersController(_mockService.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            string sortOrder = null;
            string searchString = null;
            
            var result = _controller.Index(searchString, sortOrder);
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
            var customer = new Customer
            {
                Name = "Test Customer",
                PhoneNo = 22222222,
                Email = "test@test.com",
                Address = "Testvej 5",
                City = "0000 By",
                DiscountGroup = "5"
            };

            var result = _controller.Create(customer);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
