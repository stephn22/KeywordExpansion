﻿using Microsoft.Extensions.Configuration;

namespace App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddApplicationCore();
        builder.Services.AddInfrastructure(builder.Configuration);

        return builder.Build();
    }
}