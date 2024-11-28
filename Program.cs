using Serilog;

namespace Weathertest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Serilog konfigurieren
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Serilog als Logging-Provider registrieren
            builder.Host.UseSerilog();
            //builder.Logging.AddSerilog(logger);


            // Services hinzufügen
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Middleware hinzufügen
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Serilog-Logging-Middleware
            app.UseSerilogRequestLogging();

            app.MapControllers();

            app.Run();
        }
    }
}
