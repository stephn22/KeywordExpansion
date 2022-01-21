using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Xml.Linq;

namespace Application.Common.Services.GoogleSuggest;

public class GoogleSuggestApi : SuggestApi
{
    private readonly string _username;
    private readonly string _password;
    private readonly string _proxyAddress;
    private readonly ILogger<GoogleSuggestApi> _logger;

    public GoogleSuggestApi(
        string username,
        string password,
        string proxyAddress,
        ILogger<GoogleSuggestApi> logger,
        int seedLength,
        ICsvFileReader csvFileReader,
        IMediator mediator,
        string? filePath = null)
        : base(seedLength, csvFileReader, mediator, filePath)
    {
        _username = username;
        _password = password;
        _proxyAddress = proxyAddress;
        _logger = logger;
    }


    public override async Task<IEnumerable<string>> GetSuggestions(string seed, string language, string country, int seedLength)
    {
        var proxy = new WebProxy(_proxyAddress)
        {
            Credentials = new NetworkCredential(_username, _password)
        };

        var clientHandler = new HttpClientHandler
        {
            Proxy = proxy,
            UseProxy = true
        };

        using var client = new HttpClient(clientHandler, true);
        var url = $"http://suggestqueries.google.com/complete/search?&output=toolbar&hl={language}&q={seed}&gl={country}";

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

        // XML parsing

        var res = await response.Content.ReadAsStringAsync();

        var doc = XDocument.Parse(res);
        var root = doc.Root;

        if (root == null)
        {
            return suggestions;
        }

        foreach (var xNode in root.Nodes())
        {
            var value = GetValue((XElement)xNode, seedLength);

            if (value != null)
            {
                suggestions.Add(value);
            }
        }

        return suggestions;
    }

    private static string? GetValue(XContainer element, int seedLength)
    {
        try
        {
            var value = element.Descendants().First().Attribute("data")?.Value;

            if (value?.Length <= seedLength)
            {
                return value;
            }
        }
        catch (InvalidOperationException)
        {
            return null;
        }

        return null;
    }
}