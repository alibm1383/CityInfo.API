using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace CityInfo.API.Services
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuraion;
        public SmsService(IConfiguration configuration)
        {
            _configuraion = configuration;
        }
        public async Task SendSmsAsync(string to, string body)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("x-api-key", "api-key");

                var data = new
                {
                    lineNumber = _configuraion["MailSettings:LineNumber"],
                    messageText = body,
                    mobiles = new[] { to },
                    sendDateTime = (string?)null
                };

                var json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://api.sms.ir/v1/send/bulk", content);
                var result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
