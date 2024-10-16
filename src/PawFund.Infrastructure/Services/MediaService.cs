using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Settings;
using Microsoft.AspNetCore.Http;
using PawFund.Contract.DTOs.MediaDTOS;

namespace PawFund.Infrastructure.Services;

public class MediaService : IMediaService
{
    private readonly CloudinarySetting _cloudinarySetting;
    private readonly Cloudinary _cloudinary;
    public MediaService(IOptions<CloudinarySetting> cloudinaryConfig)
    {
        var account = new Account(cloudinaryConfig.Value.CloudName,
            cloudinaryConfig.Value.ApiKey,
            cloudinaryConfig.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
        _cloudinarySetting = cloudinaryConfig.Value;
    }

    public async Task<bool> DeleteFileAsync(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var isCheck = await _cloudinary.DestroyAsync(deletionParams);
        if (isCheck.Error != null) return false;
        return true;
    }

    public async Task<ImageDTO> UploadImage(string fileName, IFormFile fileImage)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileImage.OpenReadStream()),
            Folder = _cloudinarySetting.Folder,
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        if (uploadResult?.StatusCode != System.Net.HttpStatusCode.OK) return null;
        var imageUrl = uploadResult.Url.AbsoluteUri;
        var imageId = uploadResult.PublicId;
        return new ImageDTO
        {
            ImageUrl = imageUrl,
            PublicImageId = imageId
        };
    }
}
