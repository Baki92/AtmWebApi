using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmWebApi.Models;
using AtmWebApi.Repositories;

namespace AtmWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AtmController : ControllerBase
    {
        AtmRepository repository;

        public AtmController()
        {
            this.repository = new AtmRepository();
        }

        [HttpPost]
        [Route("withdrawal")]
        public Banknotes Withdrawal(int amount)
        {
            return this.repository.Withdrawal(amount);
        }
        [HttpPost]
        [Route("deposit")]
        public Banknotes Deposit(Banknotes bankNotes)
        {
            return this.repository.Deposit(bankNotes);
        }
    }
}
