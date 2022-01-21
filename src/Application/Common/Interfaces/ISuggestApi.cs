namespace Application.Common.Interfaces;

public interface ISuggestApi
{
    public Task Suggest(int depth);
    public Task Suggest(int depth, string keyword, string language, string country);
    public Task GetKeywords(string seed, string language, string country, int depth);
    public Task<IEnumerable<string>> GetSuggestions(string seed, string language, string country, int seedLength);
}