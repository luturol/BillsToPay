using System;
using System.Collections.Generic;
using System.Linq;

namespace BillsToPay.Models
{
    public class BillDTO
    {
        public string Name { get; set; }
        public decimal OriginalPrice { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PayDate { get; set; }
    }
}