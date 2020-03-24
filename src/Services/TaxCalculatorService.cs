using ApiClient.Models;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace ApiClient.Services
{
    // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
    public interface ITaxCalculatorService
    {
        Task<TaxRateLocation> GetRateByLocation(string zip);
        Task<TaxRateOrder> GetRateByOrder(string transactionId);
    }

    public class TaxCalculatorService : ITaxCalculatorService
    {
        private readonly RestClient _restClient;
        private readonly string _authorization;
        private readonly ILogger _logger;

        public TaxCalculatorService(TaxCalculator taxCalculator, ILogger logger)
        {

            _restClient = new RestClient(taxCalculator.BaseUrl);
            _authorization = taxCalculator.Authoriztion;
            _logger = logger;
        }

        public async Task<TaxRateLocation> GetRateByLocation(string zip) // TODO: parameter could eventually become a whole class with parameters from https://developers.taxjar.com/api/reference/#get-show-tax-rates-for-a-location
        {
            try
            {
                // TODO: verify zip is good via VerifyZip function that returns 
                // TODO: the AddOnUrls should be part of the TaxCalculator
                var addOnUrl = "rates/" + zip;
                var request = new RestRequest(addOnUrl, DataFormat.Json);
                request.AddHeader("Authorization", _authorization);
                var locationResponse = await _restClient.GetAsync<LocationResponse>(request);
                //var httpResponseMessage = await _httpService.GetAsync(addOnUrl);
                //var response = await httpResponseMessage.Content.ReadAsStringAsync();
                //var locationResponse = JsonConvert.DeserializeObject<LocationResponse>(response);

                return locationResponse.Rate;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }

        }

        public async Task<TaxRateOrder> GetRateByOrder(string transactionId)
        {
            try
            {
                // TODO: verify transactionId is good??
                // TODO: the AddOnUrls should be part of the TaxCalculator
                var addOnUrl = "transactions/orders/" + transactionId;
                var request = new RestRequest(addOnUrl, DataFormat.Json);
                request.AddHeader("Authorization", _authorization);
                var orderResponse = await _restClient.GetAsync<OrderResponse>(request);
                //var httpResponseMessage = await _httpService.GetAsync(addOnUrl);
                //var response = await httpResponseMessage.Content.ReadAsStringAsync();
                //var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(response);

                return orderResponse.Order;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }

        }

    }
}
