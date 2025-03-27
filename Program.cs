using Microsoft.EntityFrameworkCore;
using TwilioSmsReceiver.Data;
using TwilioSmsReceiver.Models;

var builder = WebApplication.CreateBuilder(args);

// Legg til DbContext for SQL Server og aktiver retry på feil
builder.Services.AddDbContext<SmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SmsDbConnection"), sqlOptions =>
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3, // Maks antall forsøk
            maxRetryDelay: TimeSpan.FromSeconds(5), // Ventetid mellom hvert forsøk
            errorNumbersToAdd: null) // Velg feilnummer for retry, eller la det være null for å inkludere alle
    )
);

// Legg til andre nødvendige tjenester
builder.Services.AddControllers();

var app = builder.Build();

// Konfigurer mellomvare
app.UseAuthorization();
app.MapControllers();

app.Run();
