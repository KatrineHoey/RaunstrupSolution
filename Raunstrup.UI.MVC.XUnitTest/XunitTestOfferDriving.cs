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
    public class XunitTestOfferDriving
    {

        private readonly Mock<IOfferDrivingService> _mockService;
        private readonly OfferDrivingControllerStub _controller;

        public XunitTestOfferDriving()
        {
            _mockService = new Mock<IOfferDrivingService>();
            _controller = new OfferDrivingControllerStub(_mockService.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnViewForIndex()
        {
            int id = 1;
            var result = _controller.Index(id);
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
            var offerDriving = new OfferDriving();
            var todaysDate = new DateTime(2020, 05, 20);

            var MockofferDriving = new OfferDriving
            {
                Id = 90,
                OfferRefId = 31,
                EmployeeRefId = 19,
                TodaysDate = todaysDate,
                Price = "4",
                Amount = 20,

            };

            var result = _controller.Create(MockofferDriving);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }

    public class OfferDrivingControllerStub : OfferDrivingController
    {
        public OfferDrivingControllerStub(IOfferDrivingService offerDrivingService) : base(offerDrivingService)
        {
            
        }

        public int UserId { get; set; }

        protected override int GetUserId()
        {
            return UserId;
        }

    }
}
