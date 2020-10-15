using System;
using System.Collections.Generic;
using System.Linq;
using BillsToPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillsToPay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillsToPayController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Bill> Get()
        {
            return new List<Bill>();
        }
    }
}
