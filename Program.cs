using Microsoft.EntityFrameworkCore;
using TwilioSmsReceiver.Data;
using TwilioSmsReceiver.Models;

var builder = WebApplication.CreateBuilder(args);

// Last inn konfigurasjonen fra appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Legg til DbContext for SQL Server og aktiver retry p� feil
builder.Services.AddDbContext<SmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SmsDbConnection"), sqlOptions =>
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3, // Maks antall fors�k
            maxRetryDelay: TimeSpan.FromSeconds(5), // Ventetid mellom hvert fors�k
            errorNumbersToAdd: null) // Velg feilnummer for retry, eller la det v�re null for � inkludere alle
    )
);

// Legg til andre n�dvendige tjenesters
builder.Services.AddControllers();

var app = builder.Build();

// Konfigurer mellomvare
app.UseAuthorization();
app.MapControllers();

app.Run();
