using ApiClient.Models;
using ApiClient.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiClient.Tests
{
    public class TaxCalculatorServiceTests
    {
        private readonly TaxCalculatorService _taxService;

        public TaxCalculatorServiceTests()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var mockLogger = new Mock<ILogger<TaxCalculatorService>>();
            ILogger<TaxCalculatorService> logger = mockLogger.Object;

            var TaxJar = new TaxCalculator(config["TaxCollectors:TaxJar:BaseUrl"], config["TaxCollectors:TaxJar:Authorization"]);
            _taxService = new TaxCalculatorService(TaxJar, logger);
        }

        [Fact]
        public async Task GetRateByLocation_ReturnsTaxRateLocation()
        {
            var taxRateLocation = await _taxService.GetRateByLocation("90404");
            Assert.NotNull(taxRateLocation);
            Assert.Equal("90404", taxRateLocation.Zip);
            Assert.Equal("CA", taxRateLocation.State);

        }

        [Fact]
        public async Task GetRateByOrder_ReturnsTaxRateOrder()
        {
            var taxRateOrder = await _taxService.GetRateByOrder("123");
            Assert.NotNull(taxRateOrder);
            Assert.Equal(142698, taxRateOrder.User_Id);
            Assert.Equal("12-34243-9", taxRateOrder.Line_Items[0].Product_Identifier);

        }
    }
}
