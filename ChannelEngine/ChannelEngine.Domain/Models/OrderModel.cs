using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Domain.Models
{
    public class OrderModel
    {
        public string ProductName { get; set; }
        public string Gtin { get; set; }
        public string MerchantProductNo { get; set; }
        public int TotalQuantity { get; set; }
    }
}
