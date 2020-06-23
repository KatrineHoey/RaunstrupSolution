using Microsoft.AspNetCore.Mvc;
using Moq;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Controllers;
using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace Raunstrup.UI.MVC.XUnitTest
{
    public class XunitTestOffer
    {
        private readonly Mock<IOfferService> _mockService;
        private readonly OfferController _controller;

        public XunitTestOffer()
        {
            _mockService = new Mock<IOfferService>();
            _controller = new OfferController(_mockService.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Index();
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
            Employee testpl = new Employee();
            Customer testcmr = new Customer();
            DateTime date1 = new DateTime(2020, 06, 06);
            DateTime date2 = new DateTime(2020, 06, 10);

            var offer = new Offer
            {
                WorkingTitle = "Renovering",
                ProjectleaderRefId = 4,
                Projectleader = testpl,
                StartDate = date1,
                EndDate = date2,
                TotalPrice = 55000,
                Description = "Renovering af vinduer",
                IsAccepted = true,
                IsDone = false,
                CustomerId = 7,
                Customer = testcmr,
                IsActive = true,
            };

            var result = _controller.Create(offer);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
