using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Domain.Models.ApiRequest
{
    public class MerchantOfferStockUpdateRequest
    {
        public string MerchantProductNo { get; set; }
        public IEnumerable<MerchantStockLocationUpdateRequest> StockLocations { get; set; }
    }
}
