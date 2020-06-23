using Moq;
using Raunstrup.Service.Api.Controllers;
using Raunstrup.Service.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Raunstrup.Service.Domain.XUnitTest
{
    public class XUnitTestOffer
    {
        [Fact]
        public void GetÓffer()
        {
            var empMock = new Mock<IEmployeeService>();
            var offerMock = new Mock<IOfferService>();


            Employee testpl = new Employee();
            Customer testcmr = new Customer();
            DateTime date1 = new DateTime(2020, 06, 06);
            DateTime date2 = new DateTime(2020, 06, 10);
            offerMock.Setup(Offer => Offer.GetOffer(It.IsAny<int>())).Returns(new Offer
            {
                Id = 1,
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
       

        });

            var offerController = new OfferController(offerMock.Object);

            var test = offerController.GetOffer(1);

            Assert.Equal(1, test.Id);
            Assert.Equal(date1, test.StartDate);
            Assert.True(test.IsActive);

        }
    }
}
