
using Microsoft.AspNetCore.Mvc;
using SentezMicro.Services.Email.Model;
using SentezMicro.Services.Email.Services;
using System;
using System.Threading.Tasks;

namespace SentezMicro.Services.Email.Controllers
{
    public class EmailController : Controller
    {
        private readonly ISentezMailService _mailService;
        public EmailController(ISentezMailService mailService)
        {
            this._mailService = mailService;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> Send([FromForm] MailModel request)
        {
            try
            {
                await _mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
