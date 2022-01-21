using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Application.Common.Services.DuckDuckGoSuggest;

public class DuckDuckGoSuggestApi : SuggestApi
{
    private readonly ILogger<DuckDuckGoSuggestApi> _logger;

    public DuckDuckGoSuggestApi(ILogger<DuckDuckGoSuggestApi> logger, int seedLength, ICsvFileReader csvFileReader, IMediator mediator, string? filePath = null) : base(seedLength, csvFileReader, mediator, filePath)
    {
        _logger = logger;
    }

    public override async Task<IEnumerable<string>> GetSuggestions(string seed, string language, string country, int seedLength)
    {
        using var client = new HttpClient();

        var url =
            $"https://duckduckgo.com/ac/?kl={country}-{language}&q={seed}";

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

        suggestions.AddRange(from JObject obj in json
                             select obj.Property("phrase") into prop
                             select prop?.Value into token
                             select token?.Value<string>() into value
                             where value?.Length <= seedLength
                             select value);

        return suggestions;
    }
}