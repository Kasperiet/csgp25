using Microsoft.AspNetCore.Mvc;
using TwilioSmsReceiver.Data;
using TwilioSmsReceiver.Models;
using Twilio;
using Twilio.TwiML;

namespace TwilioSmsReceiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly SmsDbContext _context;

        public SmsController(SmsDbContext context)
        {
            _context = context;

            // Hardkode Twilio-konfigurasjonen her
            string accountSid = "AC493608a2610bf9b0c3ad3c99a1807075"; 
            string authToken = "ab09bf575ebe8a11c6a0081e67defce9"; 
            string twilioPhoneNumber = "+19205364128"; // Denne kan brukes til å sende SMS også, hvis ønskelig

            // Initialiser Twilio-klienten med hardkodede verdier
            TwilioClient.Init(accountSid, authToken);
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
