using Microsoft.EntityFrameworkCore;
using TwilioSmsReceiver.Models;

namespace TwilioSmsReceiver.Data
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options) { }

        public DbSet<SmsMessage> SmsMessages { get; set; }
    }
}
