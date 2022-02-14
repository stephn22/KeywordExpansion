namespace App.Services;

public static class FileUpload
{
    public static async Task<string> ProcessUploadedFile(IFormFile file)
    {
        var path = Path.GetTempFileName();

        await using var stream = File.Create(path);
        await file.CopyToAsync(stream);

        return path;
    }
}