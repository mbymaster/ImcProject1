using ApiClient.Controllers;
using ApiClient.Models;
using ApiClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiClient.Tests.UnitTests
{
    public class TaxCalculatorControllerTests
    {
        private readonly TaxCalculatorController _controller;

        public TaxCalculatorControllerTests()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var mockLogger = new Mock<ILogger<TaxCalculatorController>>();
            ILogger<TaxCalculatorController> logger = mockLogger.Object;

            _controller = new TaxCalculatorController(config, logger);

            var taxRateLocation = new TaxRateLocation()
            {
                Zip = "90404",
                State = "CA"
            };

            var mockTaxCalculatorService = new Mock<ITaxCalculatorService>(logger);
            mockTaxCalculatorService.Setup(svc => svc.GetRateByLocation("90404")).Returns(Task.FromResult(taxRateLocation));
            // TODO: refactor the controller to DI TaxCalculatorService.

        }

        //[Fact]
        //public async Task GetTaxRateByZip_CanGetLocation()
        //{
        //    //        {
        //    //            "rate": {
        //    //                "zip": "90404",
        //    //"state": "CA",
        //    //"state_rate": "0.0625",
        //    //"county": "LOS ANGELES",
        //    //"county_rate": "0.01",
        //    //"city": "SANTA MONICA",
        //    //"city_rate": "0.0",
        //    //"combined_district_rate": "0.025",
        //    //"combined_rate": "0.0975",
        //    //"freight_taxable": false
        //    //            }
        //    //        }

        //    var response = await _controller.GetTaxRateByZip("90404");

        //    Assert.NotNull(response);

        //    var ok = response.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
        //    Assert.Equal(ok.StatusCode, (int)HttpStatusCode.OK);

        //    var taxRateLocation = (TaxRateLocation)ok.Value;
        //    Assert.Equal("90404", taxRateLocation.Zip);
        //    Assert.Equal("CA", taxRateLocation.State);

        //}

    }
}
