using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Application.Common.Services.BingSuggest;

public class BingSuggestApi : SuggestApi
{
    private readonly ILogger<BingSuggestApi> _logger;

    public BingSuggestApi(
        ILogger<BingSuggestApi> logger,
        int seedLength,
        ICsvFileReader csvFileReader,
        IMediator mediator,
        string? filePath = null)
        : base(seedLength, csvFileReader, mediator, filePath)
    {
        _logger = logger;
    }

    public override async Task<IEnumerable<string>> GetSuggestions(string seed, string language, string country, int seedLength)
    {
        using var client = new HttpClient();

        var url = $"https://api.bing.com/osjson.aspx?query={seed}&mkt={language}-{country.ToUpperInvariant()}";

        using var response = await client.GetAsync(url);

        var suggestions = new List<string>();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("STATUS: {@StatusCode}", response.StatusCode);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                TooManyRequestsCounter++;
            }

            return suggestions;
        }

        var res = await response.Content.ReadAsStringAsync();
        var json = JArray.Parse(res);

        foreach (var values in json)
        {
            if (values.HasValues)
            {
                suggestions.AddRange(values.Values<string?>().Where(value => value?.Length >= seedLength)!);
            }
        }

        return suggestions;
    }
}