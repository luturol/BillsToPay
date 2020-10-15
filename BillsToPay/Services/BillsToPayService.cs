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

        private decimal Interest(int delayedDays) => delayedDays <= 3 ? 0.1m : delayedDays > 3 && delayedDays < 5 ? 0.2m : 0.3m;
        private decimal Fine(int delayedDays) => delayedDays <= 3 ? 2 : delayedDays > 3 && delayedDays < 5 ? 3 : 5;
        private decimal ActualPrice(decimal originalPrice, decimal interest, decimal fine) => 
            originalPrice + ((originalPrice * interest) / 100) + ((originalPrice * fine) / 100);
        
        public List<Bill> GetBills()
        {
            return new List<Bill>();
        }


    }
}