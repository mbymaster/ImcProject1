using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClient.Models
{
    public class OrderResponse
    {
        public TaxRateOrder Order { get; set; }
    }

    public class TaxRateOrder
    {
        public string Transaction_ID { get; set; }
        public int User_Id { get; set; }
        public DateTime Transaction_Date { get; set; }
        public string Provider { get; set; }
        public string To_Country { get; set; }
        public string To_Zip { get; set; }
        public string To_State { get; set; }
        public string To_City { get; set; }
        public string To_Street { get; set; }
        public string Amount { get; set; }
        public string Shipping { get; set; }
        public string Sales_tax { get; set; }
        public List<LineItem> Line_Items { get; set; }
    }

    public class LineItem
    {
        public string ID { get; set; }
        public int Quantity { get; set; }
        public string Product_Identifier { get; set; }
        public string Description { get; set; }
        public string Unit_Price { get; set; }
        public string Discount { get; set; }
        public string Sales_Tax { get; set; }
    }
}
