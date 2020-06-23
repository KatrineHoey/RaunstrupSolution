using Microsoft.AspNetCore.Mvc;
using Moq;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Raunstrup.UI.MVC.Controllers;

namespace Raunstrup.UI.MVC.XUnitTest
{
    public class XUnitTestAssignedItem
    {
        private readonly Mock<IOfferAssignedItemService> _mockService;
        private readonly OfferAssignedItemController _controller;
        private readonly Mock<IItemService> _itemService;

        public XUnitTestAssignedItem()
        {
            _mockService = new Mock<IOfferAssignedItemService>();
            _itemService = new Mock<IItemService>();
            _controller = new OfferAssignedItemController(_mockService.Object, _itemService.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            int id = 1;

            var result = _controller.Index(id);

            Assert.IsType<ViewResult>(result.Result);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            string searchString = null;
            
            var result = _controller.Create(searchString);

            Assert.IsType<ViewResult>(result.Result);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToIndexAction()
        {
            var offerAssignedItem = new OfferAssignedItem
            {
                Id = 55,
                OfferRefId = 23,
                Name = "Gasflaske",
                Amount = 4,
                OfferPricePer = 299,
                MeasuringUnit = "liter",
            };

            var result = _controller.Create(offerAssignedItem);


            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


    }
}
