using Ecommerce.ViewModels.Momo;

namespace Ecommerce.Services
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfo model);
        MomoOptionModel Options { get; }
    }

    public class OrderInfo
    {
        public string FullName { get; set; }
        public string OrderId { get; set; }
        public string OrderInfoContent { get; set; }
        public double Amount { get; set; }
    }
}
