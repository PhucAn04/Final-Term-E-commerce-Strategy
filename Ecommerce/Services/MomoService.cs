using System.Security.Cryptography;
using System.Text;
using Ecommerce.ViewModels.Momo;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Ecommerce.Services
{
    public class MomoService : IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;

        public MomoService(IOptions<MomoOptionModel> options)
        {
            _options = options;
        }

        public MomoOptionModel Options => _options.Value;

        public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfo model)
        {
            model.OrderId = DateTime.UtcNow.Ticks.ToString();
            model.OrderInfoContent = "Khach hang: " + model.FullName;

            // RawHash phải đúng thứ tự a-z: accessKey -> amount -> extraData ...
            string rawHash = "accessKey=" + _options.Value.AccessKey +
                "&amount=" + model.Amount +
                "&extraData=" +
                "&ipnUrl=" + _options.Value.NotifyUrl +
                "&orderId=" + model.OrderId +
                "&orderInfo=" + model.OrderInfoContent +
                "&partnerCode=" + _options.Value.PartnerCode +
                "&redirectUrl=" + _options.Value.ReturnUrl +
                "&requestId=" + model.OrderId +
                "&requestType=" + _options.Value.RequestType;

            string signature = ComputeHmacSha256(rawHash, _options.Value.SecretKey);

            var requestData = new
            {
                partnerCode = _options.Value.PartnerCode,
                partnerName = "MyEstore",
                storeId = "MyEstore",
                requestId = model.OrderId,
                amount = model.Amount,
                orderId = model.OrderId,
                orderInfo = model.OrderInfoContent,
                redirectUrl = _options.Value.ReturnUrl,
                ipnUrl = _options.Value.NotifyUrl,
                lang = "vi",
                extraData = "",
                requestType = _options.Value.RequestType,
                signature = signature
            };

            using var client = new HttpClient();
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var createPaymentLink = await client.PostAsync(_options.Value.MomoApiUrl, requestContent);
            var responseContent = createPaymentLink.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(responseContent);
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] hashBytes;
            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }
            var hashString = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                hashString.Append(b.ToString("x2"));
            }
            return hashString.ToString();
        }
    }
}
