using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Services.Abstractions;

public interface IPhotoService
{
    Task<PhotoUploadedResult> AddPhotoAsync(IFormFile file, string folderName = "GymGym");

    Task<ImageUploadResult> AddPhotoFullPathAsync(IFormFile file, string folderName = "GymGym");
    Task<RawUploadResult> UploadPdfAsync(IFormFile file, string folderName = "GymGym");
    Task<bool> DeletePdfAsync(string publicId);

    Task<bool> DeletePhotoAsync(string publicId);
}
