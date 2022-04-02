using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Domain.Models.ApiResponse
{
    public class CollectionOfMerchantOrderResponse
    {
        public IEnumerable<MerchantOrderResponse> Content { get; set; }
    }
}
