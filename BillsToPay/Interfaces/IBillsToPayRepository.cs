using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillsToPay.Models;

namespace BillsToPay.Interfaces
{
    public interface IBillsToPayRepository
    {        
        Task<Bill> AddBill(Bill bill);
        Task<IEnumerable<Bill>> GetBills();
    }
}