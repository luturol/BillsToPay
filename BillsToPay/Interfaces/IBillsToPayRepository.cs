using System;
using System.Collections.Generic;
using System.Linq;
using BillsToPay.Models;

namespace BillsToPay.Interfaces
{
    public interface IBillsToPayRepository
    {
        public List<Bill> GetBills();
    }
}