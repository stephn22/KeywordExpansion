using Application.Common.Interfaces;
using MediatR;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Application.Common.Services.Suggest.DuckDuckGoSuggest;

public class DuckDuckGoSuggestApi : SuggestApi
{
    public DuckDuckGoSuggestApi(
        int seedLength,
        IMediator mediator,
        ICsvFileReader? csvFileReader = default,
        string? filePath = null)
        : base(seedLength, mediator, csvFileReader, filePath)
    { }

    public override async Task<IEnumerable<string>> GetSuggestions(string seed, string language, string country, int seedLength)
    {
        using var client = new HttpClient();

        var url =
            $"https://duckduckgo.com/ac/?kl={country}-{language}&q={seed}";

        using var response = await client.GetAsync(url);

        var suggestions = new List<string>();

        if (!response.IsSuccessStatusCode)
        {
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