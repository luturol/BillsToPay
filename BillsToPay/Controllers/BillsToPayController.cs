using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillsToPay.Interfaces;
using BillsToPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillsToPay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillsToPayController : ControllerBase
    {
        private IBillsToPayService service;

        public BillsToPayController(IBillsToPayService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var response = await service.GetBills();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(BillDTO bill)
        {
            try
            {
                var response = await service.AddBill(bill);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
