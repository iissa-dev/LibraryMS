using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace LibraryMS.Infrastructure.Services;

public class FileService(IWebHostEnvironment environment) : IFileService
{
    public void DeleteImage(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath)) return;

        var fullPath = Path.Combine(environment.WebRootPath, imagePath.TrimStart('/'));
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    public async Task<string> UploadImageAsync(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0) return string.Empty;

        var uploadsFolder = Path.Combine(environment.WebRootPath, "images", folderName);
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);


        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return $"/images/{folderName}/{uniqueFileName}";
    }
}