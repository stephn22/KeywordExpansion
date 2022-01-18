namespace Domain.Entities;

public class Ad : BaseEntity
{
    public IList<(string Title, string SubTitle, string Url, string RegistrableDomain)> Ads { get; init; }
    public IList<(string Name, string Price, string RegistrableDomain)> Products { get; init; }
}