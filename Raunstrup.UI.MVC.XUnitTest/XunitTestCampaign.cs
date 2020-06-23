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
    public class XunitTestCampaign
    {
        private readonly Mock<ICampaignService> _mockService;
        private readonly CampaignController _controller;

        public XunitTestCampaign()
        {
            _mockService = new Mock<ICampaignService>();
            _controller = new CampaignController(_mockService.Object);
        }

        [Fact]
        public void Index_ActionExcutes_ReturnViewForIndex()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result.Result);
        }

        [Fact]
        public void Create_ActionExutes_ReturnViewForCreate()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToIndexAction()
        {
            DateTime startDate = new DateTime(2020, 06, 06);
            DateTime EndDate = new DateTime(2020, 06, 07);


            var campaign = new Campaign()
            {
                Title = "Corona-Kampagne",
                Procent = 20,
                StartDate = startDate,
                EndDate = EndDate
            };

            var result = _controller.Create(campaign);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

    }
}
