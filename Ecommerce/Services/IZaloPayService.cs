namespace Ecommerce.Services
{
    public interface IZaloPayService
    {
        Task<CreateZaloPayOrderResponse> CreateOrderAsync(string fullName, long amount, string transId);
    }

    public class CreateZaloPayOrderResponse
    {
        public int ReturnCode { get; set; }
        public string OrderUrl { get; set; }
        public string ReturnMessage { get; set; }
    }
}
