using System;
using System.Collections.Generic;
using System.Linq;
using BillsToPay.Models;

namespace BillsToPay.Interfaces
{
    public interface IBillsToPayRepository
    {
        List<Bill> GetBills();
        void AddBill(Bill bill);
    }
}