using Serilog;

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console()
  .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    // Fix: Use AddSerilog and configure Serilog explicitly    
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        // Fix: Ensure Serilog.Settings.Configuration package is installed  
        configuration.ReadFrom.Configuration(context.Configuration)
                     .ReadFrom.Services(services)
                     .WriteTo.Console();
    });

    var app = builder.Build();

    // Fix: Ensure the Serilog.AspNetCore package is installed to use this extension method
    app.UseSerilogRequestLogging(); // This requires the Serilog.AspNetCore package

    app.MapGet("/", () => "Hello World!");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}