namespace TwilioSmsReceiver.Models
{
    public class SmsMessage
    {
        public int Id { get; set; }
        public string FromNumber { get; set; }
        public string MessageBody { get; set; }
        public DateTime ReceivedAt { get; set; } = DateTime.Now;
    }
}
