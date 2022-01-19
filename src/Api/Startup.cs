using Application;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Infrastructure;

namespace Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddElectron();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHsts();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        if (HybridSupport.IsElectronActive)
        {
            ElectronBootstrap();
        }
    }

    private async void ElectronBootstrap()
    {
        var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
        {
            Width = 1152,
            Height = 940,
            Show = false
        });

        await browserWindow.WebContents.Session.ClearCacheAsync();

        browserWindow.OnReadyToShow += () => browserWindow.Show();
        browserWindow.SetTitle("Keyword Expansion");
        browserWindow.RemoveMenu();
        browserWindow.LoadURL("https://localhost:3000/");
    }
}