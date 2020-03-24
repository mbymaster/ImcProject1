﻿using ImcProject1.Abstractions;
using ImcProject1.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImcProject1.Services
{
    public class TaxCalculator // <T>, Reference the GetRate(string addOnUrl, string parameter) method
    {
        private readonly TaxJarHttpService _httpService;
        private readonly ILogger _logger;

        public TaxCalculator(TaxJarHttpService httpService, ILogger logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<TaxRateLocation> GetRateByLocation(string zip) // TODO: parameter could eventually become a whole class with parameters from https://developers.taxjar.com/api/reference/#get-show-tax-rates-for-a-location
        {
            try
            {
                // TODO: verify zip is good via VerifyZip function that returns 
                var addOnUrl = "rates/" + zip;
                var httpResponseMessage = await _httpService.GetAsync(addOnUrl);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var locationResponse = JsonConvert.DeserializeObject<LocationResponse>(response);

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
                var addOnUrl = "transactions/orders/" + transactionId;
                var httpResponseMessage = await _httpService.GetAsync(addOnUrl);
                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                var orderResponse= JsonConvert.DeserializeObject<OrderResponse>(response);

                return orderResponse.Order;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }

        }

        // TODO: was thinking of way to call this using generics
        //public async Task<T> GetRate(string addOnUrl, string parameter)
        //{
        //    try
        //    {
        //        var url = addOnUrl + parameter;
        //        var httpResponseMessage = await _httpService.GetAsync(url);
        //        using var responseStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        //        var taxRateT = await JsonSerializer.DeserializeAsync<T>(responseStream);

        //        return taxRateT;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation(ex.Message);
        //        throw;
        //    }
        //
        //}
    }
}