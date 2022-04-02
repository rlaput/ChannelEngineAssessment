namespace ChannelEngine.Domain.Models.ApiResponse
{
    public class MerchantOrderLineResponse
    {
        public string Gtin { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string MerchantProductNo { get; set; }
    }
}