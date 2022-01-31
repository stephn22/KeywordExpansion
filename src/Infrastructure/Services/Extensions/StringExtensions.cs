namespace Infrastructure.Services.Extensions;

public static class StringExtensions
{
    public static string? RemoveDotCharacter(this string? text)
    {
        return text?.Replace(" •", "");
    }
}