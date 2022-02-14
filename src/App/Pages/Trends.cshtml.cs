using Application.Common.Services.Util;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Infrastructure.Services.Driver.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Pages;

public class TrendsModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly ILogger<TrendsModel> _logger;
    private readonly IConfiguration _configuration;

    public TrendsModel(IMediator mediator, ILogger<TrendsModel> logger, IConfiguration configuration)
    {
        _mediator = mediator;
        _logger = logger;
        _configuration = configuration;
    }

    public SelectList Cultures => new(Utilities.CultureList(), "Key", "Value");

    [BindProperty] public InputModel Input { get; set; }

    public class InputModel
    {
        public string Culture { get; set; }
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            using var driver = ChromiumDriverExtensions.GetChromiumDriver();
            await driver.ExplorePage(_mediator, Input.Culture);
        }
        catch (Exception e)
        {
            _logger.LogError("{@Exception}", e);

            HttpContext.Session.SetString("errorMessage", e.Message);

            Electron.Notification
                .Show(new NotificationOptions(_configuration["AppName"], "La ricerca delle keyword con Google Realtime Trends è terminata con errore")
                {
                    Icon = _configuration["AppIconPath"],
                    OnClick = () => Electron.App.Focus()
                });

            return RedirectToPage("/Keywords");
        }

        Electron.Notification
            .Show(new NotificationOptions(_configuration["AppName"], "La ricerca delle keyword con Google Realtime Trends è terminata con successo")
            {
                Icon = _configuration["AppIconPath"],
                OnClick = () => Electron.App.Focus()
            });

        return RedirectToPage("/Keywords");
    }
}