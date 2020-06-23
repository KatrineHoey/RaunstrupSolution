using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Controllers;
using Raunstrup.UI.MVC.Models;
using System;
using System.Security.Claims;
using System.Security.Principal;
using Xunit;

namespace Raunstrup.UI.MVC.XUnitTest
{
    public class XUnitTestWorkingHours
    {
        private readonly Mock<IOfferWorkingHoursService> _mockService;
        private readonly OfferWorkingHoursControllerStub _controller;
        private Mock<IIdentity> mockIdentity;

        public XUnitTestWorkingHours()
        {
            _mockService = new Mock<IOfferWorkingHoursService>();
            _controller = new OfferWorkingHoursControllerStub(_mockService.Object);
  

 

        }

        [Fact]
        public void Index_ActionExecutes_ReturnViewForIndex()
        {

            int id = 1;
            _controller.UserId = 1;
            var result = _controller.Index(id);
            Assert.IsType<ViewResult>(result.Result);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {

            _controller.UserId = 1;
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToIndexAction()
        {
            var todaysDate = new DateTime(2020, 05, 20);

            var MockofferWorkingHours = new OfferWorkingHours
            {
                Id = 90,
                OfferRefId = 31,
                EmployeeRefId = 19,
                DateOfWorking = todaysDate,
                HourlyPrice = 170,

            };

            var result = _controller.Create(MockofferWorkingHours);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }

    public class OfferWorkingHoursControllerStub : OfferWorkingHoursController
    {
        public OfferWorkingHoursControllerStub(IOfferWorkingHoursService offerWorkingHoursService) : base(offerWorkingHoursService)
        {
 
        }

        public int UserId { get; set; }

        protected override int GetUserId()
        {
            return UserId;
        }

        protected override bool IsInRole(string role)
        {
            return true;
        }
    }
}
