using Microsoft.AspNetCore.Http;

namespace LibraryMS.Application.Common.Interfaces;

public interface IFileService
{
    Task<string> UploadImageAsync(IFormFile file, string folderName);
    void DeleteImage(string imagePath);
}