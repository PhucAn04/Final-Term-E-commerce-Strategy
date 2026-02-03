using Ecommerce.Helpers;
using Newtonsoft.Json;

namespace Ecommerce.Services
{
    public class ZaloPayService : IZaloPayService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ZaloPayService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<CreateZaloPayOrderResponse> CreateOrderAsync(string fullName, long amount, string transId)
        {
            var appId = _configuration["ZaloPay:AppId"];
            var key1 = _configuration["ZaloPay:Key1"];
            var createOrderUrl = _configuration["ZaloPay:CreateOrderUrl"];

            var redirectUrl = "https://localhost:7098/Cart/SuccessZaloPay"; //đổi domain khi deploy

            var embedData = new { redirecturl = redirectUrl };
            var items = "[{}]";

            var appTime = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

            var param = new Dictionary<string, string>();
            param.Add("app_id", appId);
            param.Add("app_user", fullName);
            param.Add("app_time", appTime);
            param.Add("amount", amount.ToString());
            param.Add("app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + transId);
            param.Add("embed_data", JsonConvert.SerializeObject(embedData));
            param.Add("item", items);
            param.Add("description", $"Thanh toan don hang #{transId}");
            param.Add("bank_code", "");

            var dataToMac = $"{appId}|{param["app_trans_id"]}|{param["app_user"]}|{param["amount"]}|{param["app_time"]}|{param["embed_data"]}|{param["item"]}";
            param.Add("mac", HmacHelper.Compute(key1, dataToMac));

            var content = new FormUrlEncodedContent(param);
            var response = await _httpClient.PostAsync(createOrderUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            var resultData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseString);

            return new CreateZaloPayOrderResponse
            {
                ReturnCode = Convert.ToInt32(resultData["return_code"]),
                OrderUrl = resultData.ContainsKey("order_url") ? resultData["order_url"].ToString() : null,
                ReturnMessage = resultData["return_message"].ToString()
            };
        }
    }
}
