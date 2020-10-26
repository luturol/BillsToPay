using System;
using System.Collections.Generic;
using System.Linq;
using BillsToPay.Models;

namespace BillsToPay.Interfaces
{
    public interface IBillsToPayService
    {
        List<BillResponseDTO> GetBills();
        void AddBill(BillDTO bill);
    }
}