using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Extensions;

public static class ContextExtensions
{
    private static IConfigurationRoot GetConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var builder = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environment}.json", true, false);

        return builder.Build();
    }

    private static string GetConnectionString(IConfiguration configuration) =>
        configuration.GetConnectionString("DefaultConnection");

    public static DbContextOptionsBuilder<ApplicationDbContext> GetOptionsBuilder()
    {
        return new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(GetConnectionString(GetConfiguration()),
            b => b.MigrationsAssembly("App"));
    }
}