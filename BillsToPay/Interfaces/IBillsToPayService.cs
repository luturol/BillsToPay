using System;
using System.Collections.Generic;
using System.Linq;
using BillsToPay.Models;

namespace BillsToPay.Interfaces
{
    public interface IBillsToPayService
    {
        List<BillResponseDTO> GetBills();
        Bill AddBill(BillDTO bill);
    }
}