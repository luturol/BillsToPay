using System;
using Xunit;
using BillsToPay.Models;
using Moq;
using BillsToPay.Interfaces;
using System.Collections.Generic;
using BillsToPay.Services;
using System.Threading.Tasks;
using System.Linq;

namespace BillsToPay.Test.Models
{
    public class DueDaysPercentTest
    {
        [Fact]
        public void Should_Be_Able_To_Validate_Till_3_Days()
        {
            var payDate = new DateTime(2020, 10, 11);
            var dueDate = new DateTime(2020, 10, 10);
            int days = Math.Max(0, (payDate - dueDate).Days);

            var isValid = DueDaysPercent.Till3Days.Validate(days);

            Assert.True(isValid);            
        }

        [Fact]
        public void Should_Be_Able_To_Validate_Between_3_And_5_Days()
        {
            var payDate = new DateTime(2020, 10, 14);
            var dueDate = new DateTime(2020, 10, 10);
            int days = Math.Max(0, (payDate - dueDate).Days);

            var isValid = DueDaysPercent.Between3And5Days.Validate(days);

            Assert.True(isValid);            
        }

        [Fact]
        public void Should_Be_Able_To_Validate_After_5_Days()
        {
            var payDate = new DateTime(2020, 10, 16);
            var dueDate = new DateTime(2020, 10, 10);
            int days = Math.Max(0, (payDate - dueDate).Days);

            var isValid = DueDaysPercent.After5Days.Validate(days);

            Assert.True(isValid);            
        }

        
        [Fact]
        public void Should_Be_Able_To_Validate_No_Due()
        {
            var payDate = new DateTime(2020, 10, 09);
            var dueDate = new DateTime(2020, 10, 10);
            int days = Math.Max(0, (payDate - dueDate).Days);

            var isValid = DueDaysPercent.NoDue.Validate(days);

            Assert.True(isValid);            
        }
    }
}
