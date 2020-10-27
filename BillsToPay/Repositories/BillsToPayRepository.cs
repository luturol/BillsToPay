using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillsToPay.Interfaces;
using BillsToPay.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BillsToPay.Repositories
{
    public class BillsToPayRepository : IBillsToPayRepository
    {
        private IConfiguration configuration;
        private string ConnectionString => configuration.GetConnectionString("postgres");

        public BillsToPayRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Bill> AddBill(Bill bill)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(BillQuery.Add,
                new
                {
                    bill.Name,
                    bill.OriginalPrice,
                    duedate = bill.DueDate.ToString("dd/MM/yyyy"),
                    paydate = bill.PayDate.ToString("dd/MM/yyyy"),
                    bill.Interest,
                    bill.Fine
                });

                connection.Close();
                if (result == 1)
                    return bill;
                else
                    throw new Exception("It's was not possible to add new Bill");
            }
        }

        public async Task<IEnumerable<Bill>> GetBills()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var list = await connection.QueryAsync<Bill>(BillQuery.GetAll);
                connection.Close();
                return list;
            }
        }
    }
}