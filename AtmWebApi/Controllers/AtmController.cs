using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmWebApi.Models;

namespace AtmWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AtmController : ControllerBase
    {
        [HttpPost]
        [Route("withdrawal")]
        public Banknotes Withdrawal(int amount)
        {
            return null;
        }
        [HttpPost]
        [Route("deposit")]
        public Banknotes Deposit(Banknotes bankNotes)
        {
            return null;
        }
    }
}
