using App.Util;
using Domain.Constants;
using Infrastructure.File;
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
        if (Input.IsGoogleSuggest)
        {
            _logger.LogInformation("Google suggest");
            var suggestApiGoogle = new GoogleSuggestApi(
                _configuration["WebShare:Username"],
                _configuration["WebShare:Password"],
                _configuration["WebShare:ProxyAddress"],
                KeywordConstants.MaxLength,
                new CsvFileReader(),
                _mediator,
                string.IsNullOrEmpty(Input.File) ? null : Input.File);

            if (!string.IsNullOrEmpty(Input.Keyword))
            {
                var language = Input.Culture[..Input.Culture.IndexOf('-')];
                var country = Input.Culture[Input.Culture.IndexOf('-')..];
                await suggestApiGoogle.Suggest(5, Input.Keyword, language, country);
            }
            else if (!string.IsNullOrEmpty(Input.File))
            {
                await suggestApiGoogle.Suggest(5);
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
                var country = Input.Culture[Input.Culture.IndexOf('-')..];
                await suggestApiBing.Suggest(5, Input.Keyword, language, country);
            }
            else if (!string.IsNullOrEmpty(Input.File))
            {
                await suggestApiBing.Suggest(5);
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
                var country = Input.Culture[Input.Culture.IndexOf('-')..];
                await suggestApiDuckDuckGo.Suggest(5, Input.Keyword, language, country);
            }
            else if (!string.IsNullOrEmpty(Input.File))
            {
                await suggestApiDuckDuckGo.Suggest(5);
            }
        }

        return RedirectToPage("/Keywords");
    }
}