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

        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(5);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddElectron();

        services.AddDatabaseDeveloperPageExceptionFilter();


        services.AddRazorPages()
            .AddRazorRuntimeCompilation()
            .AddFluentValidation();

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
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

        app.UseSession();

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

    private static async void ElectronBootstrap(IHostEnvironment env)
    {
        var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
        {
            MinHeight = 600,
            MinWidth = 200,
            Center = true,
            Show = false,
            Icon = "/pictures/graph_up.svg"
        });

        await browserWindow.WebContents.Session.ClearCacheAsync();
        
        Electron.NativeTheme.SetThemeSource(ThemeSourceMode.System);

        browserWindow.OnReadyToShow += () => browserWindow.Show();
        browserWindow.SetTitle("Keyword Expansion");
        browserWindow.Maximize();
    }
}