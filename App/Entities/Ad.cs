namespace App.Entities;

public class Ad
{
    public IList<(string Title, string SubTitle, string Url, string RegistrableDomain)> Ads { get; init; }
    public IList<(string Name, string Price, string RegistrableDomain)> Products { get; init; }
}