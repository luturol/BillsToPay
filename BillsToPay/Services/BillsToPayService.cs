using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<BillResponseDTO> GetBills()
        {
            return repository.GetBills().Select(e => new BillResponseDTO
            {
                DelayedDays = DelayedDays(e.DueDate, e.PayDate),
                Name = e.Name,
                PayDate = e.PayDate,
                OriginalPrice = e.OriginalPrice,
                ActualPrice = ActualPrice(e.OriginalPrice, e.Interest, e.Fine)
            }).ToList();
        }

        public void AddBill(BillDTO bill)
        {             
            repository.AddBill(Bill.Of(bill.Name, bill.OriginalPrice, bill.DueDate, bill.PayDate));
        }
    }
}