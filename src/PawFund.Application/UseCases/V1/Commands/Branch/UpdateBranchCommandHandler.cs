using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Branchs;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.DTOs.MediaDTOs;
using Microsoft.AspNetCore.Http;

namespace PawFund.Application.UseCases.V1.Commands.Branch;
public sealed class UpdateBranchCommandHandler : ICommandHandler<Command.UpdateBranchCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IMediaService _mediaService;

    public UpdateBranchCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IEFUnitOfWork efUnitOfWork, IMediaService mediaService)
    {
        _branchRepository = branchRepository;
        _efUnitOfWork = efUnitOfWork;
        _mediaService = mediaService;
    }

    public async Task<Result> Handle(Command.UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branchFound = await _branchRepository.FindByIdAsync(request.Id);
        if (branchFound == null || branchFound.IsDeleted == true)
        {
            throw new BranchException.BranchNotFoundException(request.Id);
        }
        //if request has image, update image
        //if request does not have image, use old image
        //imageUpdate = newImage if has image OR oldImage if has no image
        ImageDTO imageUpdate = new ImageDTO();
        if(request.Image != null)
        {
            //Delete Image in Cloudinary
            await _mediaService.DeleteFileAsync(branchFound.PublicImageId);
            var imageUploadCloudinary = await _mediaService.UploadImagesAsync(new List<IFormFile> { request.Image });
            imageUpdate.ImageUrl = imageUploadCloudinary[0].ImageUrl;
            imageUpdate.PublicImageId = imageUploadCloudinary[0].PublicImageId;
        }
        else
        {
            imageUpdate.ImageUrl = branchFound.ImageUrl;
            imageUpdate.PublicImageId= branchFound.PublicImageId;
        }

        //Update Branch
        branchFound.UpdateBranch(request.Name, request.PhoneNumberOfBranch, request.EmailOfBranch, request.Description, request.NumberHome, request.StreetName, request.Ward, request.District, request.Province, request.PostalCode, imageUpdate.ImageUrl, imageUpdate.PublicImageId, branchFound.AccountId, DateTime.Now, DateTime.Now, false);
        _branchRepository.Update(branchFound);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);

        //Return Result
        return Result.Success(new Success(MessagesList.BranchUpdateBranchSuccess.GetMessage().Code, MessagesList.BranchUpdateBranchSuccess.GetMessage().Message));
    }
}

