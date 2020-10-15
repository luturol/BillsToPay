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

        public Bill(string name, decimal originalPrice, DateTime dueDate, DateTime payDate)
        {
            Name = name;
            OriginalPrice = originalPrice;
            DueDate = dueDate;
            PayDate = payDate;
        }
    }
}