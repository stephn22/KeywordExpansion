using App.Util;
using Application.Common.Interfaces;
using Application.Common.Services.BingSuggest;
using Application.Common.Services.DuckDuckGoSuggest;
using Application.Common.Services.GoogleSuggest;
using Application.Keywords.Queries.GetKeywords;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.File;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;

namespace App.Pages;

public class SuggestModel : PageModel
{
    private readonly IMediator _mediator;
    private ISuggestApi _suggestApi;
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

    public IEnumerable<Keyword> Keywords { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var keywords = await _mediator.Send(new GetKeywordsQuery());
            Keywords = keywords.ToList();
        }
        catch (SqliteException)
        {
            Keywords = new List<Keyword>();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Input.IsGoogleSuggest)
        {
            _suggestApi = new GoogleSuggestApi(
                _configuration["WebShare:Username"],
                _configuration["WebShare:Password"],
                _configuration["WebShare:ProxyAddress"],
                KeywordConstants.MaxLength,
                new CsvFileReader(),
                _mediator,
                string.IsNullOrEmpty(Input.File) ? null : Input.File);
        }

        if (Input.IsBingSuggest)
        {
            _suggestApi = new BingSuggestApi(
                KeywordConstants.MaxLength,
                new CsvFileReader(),
                _mediator,
                string.IsNullOrEmpty(Input.File) ? null : Input.File);
        }

        if (Input.IsDuckDuckGoSuggest)
        {
            _suggestApi = new DuckDuckGoSuggestApi(
                KeywordConstants.MaxLength,
                new CsvFileReader(),
                _mediator,
                string.IsNullOrEmpty(Input.File) ? null : Input.File);
        }

        if (!string.IsNullOrEmpty(Input.Keyword))
        {
            var language = Input.Culture[..Input.Culture.IndexOf('-')];
            var country = Input.Culture[Input.Culture.IndexOf('-')..];
            await _suggestApi.Suggest(5, Input.Keyword, language, country);
        }
        else if (!string.IsNullOrEmpty(Input.File))
        {
            await _suggestApi.Suggest(5);
        }

        return Page();
    }
}