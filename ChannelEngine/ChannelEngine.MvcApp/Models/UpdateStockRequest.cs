using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngine.MvcApp.Models
{
    public class UpdateStockRequest
    {
        public string MerchantProductNo { get; set; }
        public int StockQuantity { get; set; }
    }
}
