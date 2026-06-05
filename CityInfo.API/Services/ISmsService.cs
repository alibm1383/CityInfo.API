
namespace CityInfo.API.Services
{
    public interface ISmsService
    {
        Task SendSmsAsync(string to, string body);
    }
}