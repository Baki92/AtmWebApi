using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmWebApi.Models;
using AtmWebApi.Repositories;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult Withdrawal(int amount)
        {
            if (amount % 1000 != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Banknotes result = this.repository.Withdrawal(amount);
            if(result==null)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("deposit")]
        public IActionResult Deposit(Banknotes bankNotes)
        {
            if (!bankNotes.Validate())
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return Ok(this.repository.Deposit(bankNotes));
        }
    }
}
