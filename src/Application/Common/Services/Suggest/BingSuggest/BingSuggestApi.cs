using System.Net;
using Application.Common.Interfaces;
using MediatR;
using Newtonsoft.Json.Linq;

namespace Application.Common.Services.Suggest.BingSuggest;

public class BingSuggestApi : SuggestApi
{
    public BingSuggestApi(
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
            $"https://api.bing.com/osjson.aspx?query={seed}&mkt={language}-{country.ToUpperInvariant()}";

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

        foreach (var values in json)
        {
            if (values.HasValues)
            {
                suggestions.AddRange(values.Values<string?>().Where(value => value?.Length <= seedLength)!);
            }
        }

        return suggestions;
    }
}