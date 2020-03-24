using ApiClient.Controllers;
using ApiClient.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ApiClient.Tests
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
        }

        [Fact]
        public async Task GetTaxRateByZip_OkResponse()
        {
                //        {
                //            "rate": {
                //                "zip": "90404",
                //"state": "CA",
                //"state_rate": "0.0625",
                //"county": "LOS ANGELES",
                //"county_rate": "0.01",
                //"city": "SANTA MONICA",
                //"city_rate": "0.0",
                //"combined_district_rate": "0.025",
                //"combined_rate": "0.0975",
                //"freight_taxable": false
                //            }
                //        }

            var response = await _controller.GetTaxRateByZip("90404");

            Assert.NotNull(response);
            
            var ok = response.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            Assert.Equal(ok.StatusCode, (int)HttpStatusCode.OK);
            
            var taxRateLocation = (TaxRateLocation)ok.Value;
            Assert.Equal("90404", taxRateLocation.Zip);
            Assert.Equal("CA", taxRateLocation.State);

        }

        [Fact]
        public async Task GetTaxRateByOrder_OkResponse()
        {
                //            {
                //                "order": {
                //                    "transaction_id": "123",
                //        "user_id": 142698,
                //        "provider": "api",
                //        "transaction_date": "2015-05-14T00:00:00.000Z",
                //        "transaction_reference_id": null,
                //        "customer_id": null,
                //        "exemption_type": null,
                //        "from_country": "US",
                //        "from_zip": null,
                //        "from_state": null,
                //        "from_city": null,
                //        "from_street": null,
                //        "to_country": "US",
                //        "to_zip": "90002",
                //        "to_state": "CA",
                //        "to_city": "LOS ANGELES",
                //        "to_street": "123 Palm Grove Ln",
                //        "amount": "16.5",
                //        "shipping": "1.5",
                //        "sales_tax": "0.95",
                //        "line_items": [
                //            {
                //                "id": 0,
                //                "quantity": 1,
                //                "product_identifier": "12-34243-9",
                //                "product_tax_code": null,
                //                "description": "Fuzzy Widget",
                //                "unit_price": "15.0",
                //                "discount": "0.0",
                //                "sales_tax": "0.95"
                //            }
                //        ]
                //    }
                //}
            var response = await _controller.GetTaxRateByOrderId("123");

            Assert.NotNull(response);

            var ok = response.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            Assert.Equal(ok.StatusCode, (int)HttpStatusCode.OK);

            var taxRateOrder = (TaxRateOrder)ok.Value;
            Assert.Equal(142698, taxRateOrder.User_Id);
            Assert.Equal("12-34243-9", taxRateOrder.Line_Items[0].Product_Identifier);

        }

    }
}
