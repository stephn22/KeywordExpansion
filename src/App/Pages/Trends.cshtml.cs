using App.Util;
using Application.Keywords.Queries.GetKeywords;
using Domain.Entities;
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

    public IEnumerable<Keyword> Keywords { get; set; }
    public SelectList Cultures => new(Utilities.CultureList(), "Key", "Value");

    [BindProperty] public InputModel Input { get; set; }

    public class InputModel
    {
        public string Culture { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Keywords = await _mediator.Send(new GetKeywordsQuery());
    }
}