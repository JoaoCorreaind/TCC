using System.Threading.Tasks;

namespace WebApplication1.Interfaces
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(string recipientEmail, string topic, string textMessage, string messageHtml);
    }
}
