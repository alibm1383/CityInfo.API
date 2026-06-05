

using System.Net.Mail;
using System.Net;

namespace CityInfo.API.Services
{
    public class GmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress("bakhtiarimoghadam1383@gmail.com", "Company");
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("bakhtiarimoghadam1383@gmail.com", "app-key");
                await client.SendMailAsync(message);
            }
        }
    }
}
