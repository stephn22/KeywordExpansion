using Application;
using ElectronNET.API;
using ElectronNET.API.Entities;
using FluentValidation.AspNetCore;
using Infrastructure;

namespace App;

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

        services.AddElectron();

        services.AddDatabaseDeveloperPageExceptionFilter();


        services.AddRazorPages()
            .AddRazorRuntimeCompilation()
            .AddFluentValidation();

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        env.EnvironmentName = "Development";
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
        });

        if (HybridSupport.IsElectronActive)
        {
            ElectronBootstrap(env);
        }
    }

    private async void ElectronBootstrap(IHostEnvironment env)
    {
        var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
        {
            MinHeight = 600,
            MinWidth = 200,
            Center = true,
            Show = false,
            Icon = Path.GetFullPath("/wwwroot/pictures/graph_up.svg")
        });

        await browserWindow.WebContents.Session.ClearCacheAsync();
        await browserWindow.WebContents.Session.ClearStorageDataAsync();
        await browserWindow.WebContents.Session.ClearHostResolverCacheAsync();

        browserWindow.OnReadyToShow += () => browserWindow.Show();
        browserWindow.SetTitle("Keyword Expansion");
        browserWindow.Maximize();

        if (!env.IsDevelopment())
        {
            browserWindow.RemoveMenu();
        }
    }
}