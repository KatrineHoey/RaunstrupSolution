using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Raunstrup.Service.Contract.Services;
using Raunstrup.UI.MVC.Controllers;
using Raunstrup.UI.MVC.Models;
using System;
using Xunit;

namespace Raunstrup.UI.MVC.XUnitTest
{
    public class XUnitTestItem
    {
        private readonly Mock<IItemService> _mockService;
        private readonly ItemsController _controller;

        public XUnitTestItem()
        {
            _mockService = new Mock<IItemService>();
            _controller = new ItemsController(_mockService.Object);
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
            Item item = new Item();
            item.ItemNo = 225;
            item.ItemName = "Træstamme";
            item.PurchasePrice = 0.50;
            item.SalePrice = 15.50;
            item.MeasuringUnit = "Meter";

            var result = _controller.Create(item);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }
    }
}
