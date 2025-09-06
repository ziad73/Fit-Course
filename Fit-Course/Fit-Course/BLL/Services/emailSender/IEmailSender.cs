using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }

}