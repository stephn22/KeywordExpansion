using Application.Keywords.Queries.GetKeywords;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

public class SuggestModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly Mediator _mediator;

    public SuggestModel(ApplicationDbContext context, Mediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public string Keyword { get; set; }
        public string File { get; set; }
    }

    public IList<Keyword> Keywords { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var keywords = await _mediator.Send(new GetKeywordsQuery());
        Keywords = keywords.ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Page();
    }
}