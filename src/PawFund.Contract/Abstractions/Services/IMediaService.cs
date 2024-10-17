using Microsoft.AspNetCore.Http;
using PawFund.Contract.DTOs.MediaDTOs;

namespace PawFund.Contract.Abstractions.Services;

public interface IMediaService
{
    Task<ImageDTO> UploadImage(string fileName, IFormFile fileImage);
    Task<bool> DeleteFileAsync(string publicId);
}
