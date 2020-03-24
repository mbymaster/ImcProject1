using ImcProject1.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImcProject1.Services
{
    public class TaxJarHttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public TaxJarHttpService(IHttpClientFactory clientFactory, ILogger logger)
        {
            _httpClient = clientFactory.CreateClient("TaxJar");
            _logger = logger;
        }

        public async Task<HttpResponseMessage> GetAsync(string urlAddOn)
        {
            try
            {
                var response = await _httpClient.GetAsync(urlAddOn);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }

        }

    }
}
