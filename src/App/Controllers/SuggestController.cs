using Application.Common.Interfaces;
using Application.Keywords.Queries.GetKeywords;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

public class GoogleSuggestController : ApiControllerBase
{
    private readonly ISuggestApi _suggest;

    public GoogleSuggestController(ISuggestApi suggest)
    {
        _suggest = suggest;
    }

    [HttpGet("{keyword:alpha}, {language:alpha}, {country:alpha}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> ReadFromSeed(string keyword, string language, string country)
    {
        await _suggest.Suggest(5, keyword, language, country);
        var keywords = await Mediator.Send(new GetKeywordsQuery
        {
            Culture = $"{language}-{country}"
        });

        return Ok("hello");
    }
}