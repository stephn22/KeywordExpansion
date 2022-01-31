using ElectronNET.API;
using Serilog;

namespace App;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("./Log/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Log.Information("Starting application");

        CreateHostBuilder(args).Build().Run();

        Log.CloseAndFlush();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseElectron(args);
                webBuilder.UseStartup<Startup>();
            });
}