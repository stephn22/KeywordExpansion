using Application.Common.Services.Util;
using Domain.Constants;
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

    public SuggestModel(IMediator mediator, IConfiguration configuration, ILogger<SuggestModel> logger)
    {
        _mediator = mediator;
        _configuration = configuration;
        _logger = logger;
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
                var suggestApiGoogle = new GoogleSuggestApi(
                    _configuration["WebShare:Username"],
                    _configuration["WebShare:Password"],
                    _configuration["WebShare:ProxyAddress"],
                    KeywordConstants.MaxLength,
                    _mediator,
                    string.IsNullOrEmpty(Input.File) ? null : Input.File);

                if (!string.IsNullOrEmpty(Input.Keyword))
                {
                    var language = Input.Culture[..Input.Culture.IndexOf('-')];
                    var country = Input.Culture[(Input.Culture.IndexOf('-') + 1)..];
                    await suggestApiGoogle.GetKeywords(Input.Keyword, language, country, Input.Depth);
                }
                else if (!string.IsNullOrEmpty(Input.File))
                {
                    await suggestApiGoogle.Suggest(Input.Depth);
                }
            }

            if (Input.IsBingSuggest) // TODO: fix
            {
                var suggestApiBing = new BingSuggestApi(
                    KeywordConstants.MaxLength,
                    _mediator,
                    string.IsNullOrEmpty(Input.File) ? null : Input.File);

                if (!string.IsNullOrEmpty(Input.Keyword))
                {
                    var language = Input.Culture[..Input.Culture.IndexOf('-')];
                    var country = Input.Culture[(Input.Culture.IndexOf('-') + 1)..];
                    await suggestApiBing.GetKeywords(Input.Keyword, language, country, Input.Depth);
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
                    _mediator,
                    string.IsNullOrEmpty(Input.File) ? null : Input.File);

                if (!string.IsNullOrEmpty(Input.Keyword))
                {
                    var language = Input.Culture[..Input.Culture.IndexOf('-')];
                    var country = Input.Culture[(Input.Culture.IndexOf('-') + 1)..];
                    await suggestApiDuckDuckGo.GetKeywords(Input.Keyword, language, country, Input.Depth);
                }
                else if (!string.IsNullOrEmpty(Input.File))
                {
                    await suggestApiDuckDuckGo.Suggest(Input.Depth);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError("{@Exception}", e);
            return RedirectToPage("/Keywords", routeValues: e.ToString());
        }

        return RedirectToPage("/Keywords");
    }
}