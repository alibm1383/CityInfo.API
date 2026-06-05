using System.Net.Mail;
using System.Net;

namespace CityInfo.API.Services
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body);
        
    }
}
