using System;
using Xunit;
using BillsToPay.Models;
using Moq;
using BillsToPay.Interfaces;
using System.Collections.Generic;
using BillsToPay.Services;
using System.Threading.Tasks;
using System.Linq;

namespace BillsToPay.Test.Serivces
{
    public class BillsToPayServiceTest
    {
        private Mock<IBillsToPayRepository> mockRepository = new Mock<IBillsToPayRepository>();

        [Fact]
        public async Task Should_Be_Able_To_Calculate_ActualPrice_Giving_Non_Zero_Due_And_InterestAsync()
        {
            IEnumerable<Bill> stub = new List<Bill>() {
                    Bill.Of("Light", 100m, new DateTime(2020, 10, 10), new DateTime(2020, 10, 16))
            };
            mockRepository.Setup(e => e.GetBills())
                .Returns(Task.FromResult(stub));

            var service = new BillsToPayService(mockRepository.Object);
            var bills = await service.GetBills();

            Assert.True(bills.ToList().Count > 0);
            Assert.Equal(bills.ToList()[0].ActualPrice, 105.3m);
        }

        [Fact]
        public async Task Should_Be_Able_To_Calculate_ActualPrice_Giving_Zero_Due_And_InterestAsync()
        {
            IEnumerable<Bill> stub = new List<Bill>() { Bill.Of("Light", 100m, new DateTime(2020, 10, 10), new DateTime(2020, 10, 10)) };
            mockRepository.Setup(e => e.GetBills())
                .Returns(Task.FromResult(stub));

            var service = new BillsToPayService(mockRepository.Object);
            var bills = await service.GetBills();

            Assert.True(bills.ToList().Count > 0);
            Assert.Equal(bills.ToList()[0].ActualPrice, 100m);
        }

        [Fact]
        public async Task Should_Be_Able_To_Get_No_Due_To_Bill_Giving_PayDate_Less_Then_DueDateAsync()
        {
            mockRepository.Setup(e => e.AddBill(It.IsAny<Bill>()));

            var service = new BillsToPayService(mockRepository.Object);

            var billReturn = await service.AddBill(new BillDTO
            {
                Name = "Water",
                OriginalPrice = 100,
                DueDate = new DateTime(2020, 10, 10),
                PayDate = new DateTime(2020, 10, 9)
            });

            mockRepository.Verify(e => e.AddBill(It.Is<Bill>(e => e.Name == "Water" && e.Interest == 0 && e.Fine == 0)), Times.AtLeastOnce());
        }

        [Fact]
        public async Task Should_Be_Able_To_Get_No_Due_To_Bill_Giving_PayDate_Between_1_And_3_Days_After_Due_Date()
        {
            mockRepository.Setup(e => e.AddBill(It.IsAny<Bill>()));
            var duePercents = DueDaysPercent.Till3Days;

            var service = new BillsToPayService(mockRepository.Object);

            var billReturn = await service.AddBill(new BillDTO
            {
                Name = "Water",
                OriginalPrice = 100,
                DueDate = new DateTime(2020, 10, 10),
                PayDate = new DateTime(2020, 10, 11)
            });

            mockRepository.Verify(e => e.AddBill(It.Is<Bill>(e => e.Name == "Water" && 
                                                                  e.Interest == duePercents.Interest && 
                                                                  e.Fine == duePercents.Fine)), Times.AtLeastOnce());
        }

        [Fact]
        public async Task Should_Be_Able_To_Get_No_Due_To_Bill_Giving_PayDate_Between_3_And_5_Days_After_Due_Date()
        {
            mockRepository.Setup(e => e.AddBill(It.IsAny<Bill>()));
            var duePercent = DueDaysPercent.Between3And5Days;

            var service = new BillsToPayService(mockRepository.Object);

            var billReturn = await service.AddBill(new BillDTO
            {
                Name = "Water",
                OriginalPrice = 100,
                DueDate = new DateTime(2020, 10, 10),
                PayDate = new DateTime(2020, 10, 14)
            });

            mockRepository.Verify(e => e.AddBill(It.Is<Bill>(e => e.Name == "Water" && 
                                                                  e.Interest == duePercent.Interest && 
                                                                  e.Fine == duePercent.Fine)), Times.AtLeastOnce());
        }

        [Fact]
        public async Task Should_Be_Able_To_Get_No_Due_To_Bill_Giving_PayDate_After_5_Days_From_Due_Date()
        {
            mockRepository.Setup(e => e.AddBill(It.IsAny<Bill>()));
            var duePercent = DueDaysPercent.After5Days;

            var service = new BillsToPayService(mockRepository.Object);

            var billReturn = await service.AddBill(new BillDTO
            {
                Name = "Water",
                OriginalPrice = 100,
                DueDate = new DateTime(2020, 10, 10),
                PayDate = new DateTime(2020, 10, 16)
            });

            mockRepository.Verify(e => e.AddBill(It.Is<Bill>(e => e.Name == "Water" && 
                                                                  e.Interest == duePercent.Interest && 
                                                                  e.Fine == duePercent.Fine)), Times.AtLeastOnce());
        }
    }
}
