using System.Collections;
using System.Collections.Generic;

namespace ChannelEngine.Domain.Models.ApiResponse
{
    public class MerchantOrderResponse
    {
        public IEnumerable<MerchantOrderLineResponse> Lines { get; set; }
    }
}