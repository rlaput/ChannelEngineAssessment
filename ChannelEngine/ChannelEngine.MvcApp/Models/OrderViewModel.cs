using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.MvcApp.Models
{
    public class OrderViewModel
    {
        public string ProductName { get; set; }
        public string Gtin { get; set; }
        public int TotalQuantity { get; set; }
        public string MerchantProductNo { get; set; }
    }
}
