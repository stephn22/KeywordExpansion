using App.Util;
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

    public TrendsModel(IMediator mediator, ILogger<TrendsModel> logger)
    {
        _mediator = mediator;
        _logger = logger;
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
        using var driver = ChromiumDriverExtensions.GetChromiumDriver();
        await driver.ExplorePage(_mediator, Input.Culture);

        return RedirectToPage("/Keywords");
    }
}