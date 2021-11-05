using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtmWebApi.Models;
using AtmWebApi.Repositories;
using AtmWebApi.Interfaces;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

using NLog;

namespace AtmWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AtmController : ControllerBase
    {
        private IAtmRepository repository;
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public AtmController()
        {
            try
            {
                this.repository = new AtmRepository();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("withdrawal")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Banknotes))]
        public IActionResult withdrawal(int amount)
        {
            if (this.repository == null)
            {
                this.logger.Error("Repository not inited");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Banknotes result = null;
            this.logger.Info("api/withdrawal called");
            this.logger.Info("api/withdrawal input param: {0}",amount);
            if (amount % 1000 != 0 || amount<=0)
            {
                this.logger.Error("Input parameter is invalid");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            try
            {
                result = this.repository.withdrawal(amount);
            }
            catch(Exception ex)
            {
                this.logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            if(result==null)
            {
                this.logger.Error("Not storing enough banknotes");
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
            this.logger.Info("Money withdrawal successfully");
            return Ok(result);
        }
        [HttpPost]
        [Route("deposit")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(int))]
        public IActionResult deposit(Banknotes bankNotes)
        {
            if (this.repository == null)
            {
                this.logger.Error("Repository not inited");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            int result = 0;
            this.logger.Info("api/deposit called");
            this.logger.Info("api/deposit input param: {0}", bankNotes.convertToJsonFromat());
            if (!bankNotes.validate())
            {
                this.logger.Error("Input parameter is invalid");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            try
            {
                result = this.repository.deposit(bankNotes);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
            this.logger.Info("Money deposit successfully");
            return Ok(result);
        }
    }
}
