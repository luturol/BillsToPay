using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillsToPay.Interfaces;
using BillsToPay.Models;

namespace BillsToPay.Services
{
    public class BillsToPayService : IBillsToPayService
    {
        private IBillsToPayRepository repository;
        public BillsToPayService(IBillsToPayRepository repository)
        {
            this.repository = repository;
        }

        private int DelayedDays(DateTime dueDate, DateTime payDate) => Math.Max(0, (payDate - dueDate).Days);
        
        private decimal ActualPrice(decimal originalPrice, decimal interest, decimal fine) =>
            originalPrice + ((originalPrice * interest) + (originalPrice * fine));

        public async Task<IEnumerable<BillResponseDTO>> GetBills()
        {
            var bills = await repository.GetBills();
            return bills.ToList().Select(e => new BillResponseDTO
            {
                DelayedDays = DelayedDays(e.DueDate, e.PayDate),
                Name = e.Name,
                PayDate = e.PayDate,
                OriginalPrice = e.OriginalPrice,
                ActualPrice = ActualPrice(e.OriginalPrice, e.Interest, e.Fine)
            });
        }

        public Task<Bill> AddBill(BillDTO bill)
        {             
            return repository.AddBill(Bill.Of(bill.Name, bill.OriginalPrice, bill.DueDate, bill.PayDate));
        }
    }
}