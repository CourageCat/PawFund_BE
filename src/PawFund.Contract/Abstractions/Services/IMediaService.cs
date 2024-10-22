using Microsoft.AspNetCore.Http;
using PawFund.Contract.DTOs.MediaDTOs;

namespace PawFund.Contract.Abstractions.Services;

public interface IMediaService
{
    Task<ImageDTO> UploadImage(string fileName, IFormFile fileImage);
    Task<List<ImageDTO>> UploadImages(List<IFormFile> fileImages);
    Task<bool> DeleteFileAsync(string publicId);
}
