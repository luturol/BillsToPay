using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillsToPay.Models;

namespace BillsToPay.Interfaces
{
    public interface IBillsToPayService
    {
        Task<IEnumerable<BillResponseDTO>> GetBills();
        Task<Bill> AddBill(BillDTO bill);
    }
}