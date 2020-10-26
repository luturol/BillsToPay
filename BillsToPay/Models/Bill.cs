using System;
using System.Collections.Generic;
using System.Linq;

namespace BillsToPay.Models
{
    public class Bill
    {
        public string Name { get; private set; }
        public decimal OriginalPrice { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime PayDate { get; private set; }
        public decimal Interest { get; private set; }
        public decimal Fine { get; private set; }

        public static Bill Of(string name, decimal originalPrice, DateTime dueDate, DateTime payDate)
        {            
            var percentDue = DueDaysPercent.Values.First(e => e.Validate((payDate - dueDate).Days));
            return new Bill(name, originalPrice, dueDate, payDate, percentDue.Interest, percentDue.Fine);
        }

        private Bill(string name, decimal originalPrice, DateTime dueDate, DateTime payDate, decimal interest, decimal fine)
        {
            Name = name;
            OriginalPrice = originalPrice;
            DueDate = dueDate;
            PayDate = payDate;
            Interest = interest;
            Fine = fine;
        }
    }
}