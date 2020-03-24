using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ImcProject1.Abstractions;
using ImcProject1.Models;
using ImcProject1.Services;
using ImcProject1.TaxCollectorServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImcProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly TaxCalculator _taxCalculator;

        public TaxCalculatorController(IHttpClientFactory clientFactory, ILogger<TaxCalculatorController> logger)
        {
            var taxJar = new TaxJarService(clientFactory, logger);
            _taxCalculator = new TaxCalculator(taxJar, logger);
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return Ok(new { Message = "This works" });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest();
            }

        }

        [HttpGet("location/{zip}")]
        public async Task<ActionResult<TaxRateLocation>> GetTaxRateByZip(string zip)
        {
            try
            {
                var taxRateLocation = await _taxCalculator.GetRateByLocation(zip);
                return Ok(taxRateLocation);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest();
            }

        }

        [HttpGet("order/{id}")]
        public async Task<ActionResult<TaxRateOrder>> GetTaxRateByOrderId(string id)
        {
            try
            {
                var taxRateOrder = await _taxCalculator.GetRateByOrder(id);
                return Ok(taxRateOrder);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest();
            }

        }

    }
}