using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClient.Models
{
    public class TaxCalculator
    {

        public TaxCalculator() { }

        public TaxCalculator(string baseUrl, string authorization)
        {
            BaseUrl = baseUrl;
            Authoriztion = authorization;
        }

        public string BaseUrl { get; set; }
        public string Authoriztion { get; set; }
        public string LocationAddOnUrl { get; set; }
        public string OrderAddOnUrl { get; set; }
    }
}
