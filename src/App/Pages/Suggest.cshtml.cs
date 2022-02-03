using Application.Common.Interfaces;
using Application.Common.Services.Util;
using Domain.Constants;
using Infrastructure.File;
using Infrastructure.Services;
using Infrastructure.Services.BingSuggest;
using Infrastructure.Services.DuckDuckGoSuggest;
using Infrastructure.Services.GoogleSuggest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Pages;

public class SuggestModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SuggestModel> _logger;
    private ISuggestApi _suggestApi;

    public SuggestModel(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public SelectList Cultures => new(Utilities.CultureList(), "Key", "Value");

    public class InputModel
    {
        public string Keyword { get; set; }
        public string File { get; set; }
        public bool IsGoogleSuggest { get; set; }
        public bool IsBingSuggest { get; set; }
        public bool IsDuckDuckGoSuggest { get; set; }
        public string Culture { get; set; }
        public int Depth { get; set; } = 1;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        

        try
        {
            if (Input.IsGoogleSuggest)
            {
                if (!string.IsNullOrEmpty(Input.Keyword))
                {
                    var language = Input.Culture[..Input.Culture.IndexOf('-')];
                    var country = Input.Culture[(Input.Culture.IndexOf('-') + 1)..];
                    _suggestApi = new SuggestApi(
                        _mediator,
                        _configuration["WebShare:Username"],
                        _configuration["WebShare:Password"],
                        _configuration["WebShare:ProxyAddress"]);
                }
                else if (!string.IsNullOrEmpty(Input.File))
                {
                    _suggestApi = new SuggestApi(
                        _mediator,
                        _configuration["WebShare:Username"],
                        _configuration["WebShare:Password"],
                        _configuration["WebShare:ProxyAddress"],
                        new CsvFileReader(),
                        Input.File);
                }
            }

            if (Input.IsBingSuggest)
            {
                var suggestApiBing = new BingSuggestApi(
                    KeywordConstants.MaxLength,
                    new CsvFileReader(),
                    _mediator,
                    string.IsNullOrEmpty(Input.File) ? null : Input.File);

                if (!string.IsNullOrEmpty(Input.Keyword))
                {
                    var language = Input.Culture[..Input.Culture.IndexOf('-')];
                    var country = Input.Culture[(Input.Culture.IndexOf('-') + 1)..];
                    await suggestApiBing.Suggest(Input.Depth, Input.Keyword, language, country);
                }
                else if (!string.IsNullOrEmpty(Input.File))
                {
                    await suggestApiBing.Suggest(Input.Depth);
                }
            }

            if (Input.IsDuckDuckGoSuggest)
            {
                var suggestApiDuckDuckGo = new DuckDuckGoSuggestApi(
                    KeywordConstants.MaxLength,
                    new CsvFileReader(),
                    _mediator,
                    string.IsNullOrEmpty(Input.File) ? null : Input.File);

                if (!string.IsNullOrEmpty(Input.Keyword))
                {
                    var language = Input.Culture[..Input.Culture.IndexOf('-')];
                    var country = Input.Culture[(Input.Culture.IndexOf('-') + 1)..];
                    await suggestApiDuckDuckGo.Suggest(Input.Depth, Input.Keyword, language, country);
                }
                else if (!string.IsNullOrEmpty(Input.File))
                {
                    await suggestApiDuckDuckGo.Suggest(Input.Depth);
                }
            }
        }
        catch (Exception)
        {
            return RedirectToPage("/Keywords");
        }

        return RedirectToPage("/Keywords");
    }
}