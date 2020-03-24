using ApiClient.Models;
using ApiClient.Services;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClient.TaxStrategy
{
    // https://code-maze.com/strategy/
    public class CustomerTaxCalculator
    {
        private ITaxCalculatorService _taxCalculatorService;
        private ILogger _logger;

        public CustomerTaxCalculator(ILogger logger) // (ITaxCalculatorService taxCalculatorService)
        {
            // customerId would reach out to a service which grabs the appropriate TaxCalculator to be used
            _logger = logger;
        }

        public void SetTaxCalculator(int customerId)
        {
            // TODO: SetCustomerTaxCalculator would reach out to a service which grabs the appropriate TaxCalculator to be used
            var taxCalculator = new TaxCalculator(); // replace new TaxCalculator() with the one from the above TODO service

            _taxCalculatorService = new TaxCalculatorService(taxCalculator, _logger);
        }

        public async Task<TaxRateLocation> GetRateByLocation(string zip) => await _taxCalculatorService.GetRateByLocation(zip);

        public async Task<TaxRateOrder> GetRateByOrder(string transactionId) => await _taxCalculatorService.GetRateByOrder(transactionId);
    }

    // use:
    // var customerTaxCalculatorContext = new CustomerTaxCollector(logger);
    // customerTaxCalculatorContext.setTaxCalculator(57);
    // var taxRateLocation = customerTaxCalculatorContext.GetRateByLocation;

}
