namespace Application.Common.Interfaces;

public interface ISuggestApi
{

    /// <summary>
    /// Espande le keyword utilizzando il csvfile reader
    /// </summary>
    /// <param name="depth">profondita' di ricerca</param>
    /// <returns></returns>
    public Task Suggest(int depth);

    /// <summary>
    /// Per ogni suggestion trovata la salva nel database se non già presente e ricorsivamente ripete (finche' la profondita
    /// richiesta non è raggiunta)
    /// </summary>
    /// <param name="seed">seme</param>
    /// <param name="language">lingua del seme</param>
    /// <param name="country">paese del seme</param>
    /// <param name="depth">profondita' di ricerca (ricorsione)</param>
    /// <returns></returns>
    public Task GetKeywords(string seed, string language, string country, int depth);

    /// <summary>
    /// Richiama il servizio di suggest (tra Google, Bing e DuckDuckGo)
    /// </summary>
    /// <param name="seed">seme di ricerca</param>
    /// <param name="language">lingua del seme</param>
    /// <param name="country">paese del seme</param>
    /// <param name="seedLength">lunghezza massima del seme di ricerca</param>
    /// <returns></returns>
    public Task<IEnumerable<string>> GetSuggestions(string seed, string language, string country, int seedLength);
}