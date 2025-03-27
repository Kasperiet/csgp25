using Microsoft.AspNetCore.Mvc;
using TwilioSmsReceiver.Data;
using TwilioSmsReceiver.Models;
using Twilio;
using Twilio.TwiML;
using Microsoft.Extensions.Options;

namespace TwilioSmsReceiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly SmsDbContext _context;
        private readonly TwilioSettings _twilioSettings;

        public SmsController(SmsDbContext context, IOptions<TwilioSettings> twilioSettings)
        {
            _context = context;
            _twilioSettings = twilioSettings.Value;
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
        }

        [HttpPost]
        public IActionResult ReceiveSms([FromForm] string from, [FromForm] string body)
        {
            // Lagre SMS i databasen
            var smsMessage = new SmsMessage
            {
                FromNumber = from,
                MessageBody = body
            };

            _context.SmsMessages.Add(smsMessage);
            _context.SaveChanges();

            // Send et svar tilbake til Twilio
            var response = new MessagingResponse();
            response.Message("Thanks for your message!");

            return Content(response.ToString(), "application/xml");
        }
    }
}
