using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtmWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AtmController : ControllerBase
    {
        [HttpPost]
        [Route("withdrawal")]
        public void Withdrawal()
        {

        }
        [HttpPost]
        [Route("deposit")]
        public void Deposit(int amount)
        {

        }
    }
}
