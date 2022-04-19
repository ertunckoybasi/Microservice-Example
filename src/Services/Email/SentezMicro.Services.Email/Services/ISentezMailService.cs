using SentezMicro.Services.Email.Model;
using System.Threading.Tasks;

namespace SentezMicro.Services.Email.Services
{
    public  interface ISentezMailService
    {
        Task SendEmailAsync(MailModel mailModel);
    }
}
