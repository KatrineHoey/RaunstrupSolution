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
    public class XUnitTestOfferUsedItem
    {
        private readonly Mock<IOfferUsedItemService> _mockService;
        private readonly OfferUsedItemControllerStub _controller;
        private readonly Mock<IItemService> _itemService;

        public XUnitTestOfferUsedItem()
        {
            _mockService = new Mock<IOfferUsedItemService>();
            _itemService = new Mock<IItemService>();
            _controller = new OfferUsedItemControllerStub(_mockService.Object, _itemService.Object);
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
            string searchString = null;

            var result = _controller.Create(searchString);

            Assert.IsType<ViewResult>(result.Result);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToIndexAction()
        {
            var offerUsedItem = new OfferUsedItem
            {
                Id = 55,
                OfferRefId = 23,
                EmployeeRefId = 22,
                Name = "Gasflaske",
                Amount = 4,
                OfferPrice = 299,
                MeasuringUnit = "liter",
            };

            var result = _controller.Create(offerUsedItem);


            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


    }

    public class OfferUsedItemControllerStub : OfferUsedItemController
    {

        public OfferUsedItemControllerStub(IOfferUsedItemService OfferUsedItemService, IItemService itemService) : base(OfferUsedItemService, itemService)
        {
        }

        public int UserId { get; set; }

        protected override int GetUserId()
        {
            return UserId;
        }

    }
}
