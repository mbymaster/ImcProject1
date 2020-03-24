using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImcProject1.Models
{
    public class LocationResponse
    {
        public TaxRateLocation Rate { get; set; }
    }

    public class TaxRateLocation
    {
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Country_Rate { get; set; }
        public string State { get; set; }
        public decimal State_Rate { get; set; }
        public string County { get; set; }
        public decimal County_Rate { get; set; }
        public string City { get; set; }
        public decimal City_Rate { get; set; }
        public decimal Combined_District_Rate { get; set; }
        public decimal Combined_Rate { get; set; }
        public bool Freigh_Taxable { get; set; }
    }
}
