using Microsoft.AspNetCore.Http;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Account;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Domain.Exceptions.AccountException;

namespace PawFund.Application.UseCases.V1.Commands.Account;

public sealed class UpdateAvatarProfileCommandHandler : ICommandHandler<Command.UpdateAvatarCommand, Success<AccountAvatarDto>>
{
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IMediaService _mediaService;

    public UpdateAvatarProfileCommandHandler
        (IEFUnitOfWork efUnitOfWork,
        IMediaService mediaService)
    {
        _efUnitOfWork = efUnitOfWork;
        _mediaService = mediaService;
    }

    public async Task<Result<Success<AccountAvatarDto>>> Handle(Command.UpdateAvatarCommand request, CancellationToken cancellationToken)
    {
        var account = await _efUnitOfWork.AccountRepository.FindByIdAsync(request.UserId);
        if (account == null) throw new AccountNotFoundException();
        if((!string.IsNullOrEmpty(account.CropAvatarId) && !string.IsNullOrEmpty(account.FullAvatarId)) == true)
        {
            var oldCropPublicId = account.CropAvatarId;
            var oldFullPublicId = account.FullAvatarId;
            await Task.WhenAll(
                _mediaService.DeleteFileAsync(oldCropPublicId),
                _mediaService.DeleteFileAsync(oldFullPublicId)
            );
        }

        var uploadImages = await _mediaService.UploadImagesAsync(new List<IFormFile> { request.CropAvatarFile, request.FullAvatarFile });

        account.UpdateAvatarProfileUser(uploadImages[0].ImageUrl, uploadImages[0].PublicImageId, uploadImages[1].ImageUrl, uploadImages[1].PublicImageId);
        await _efUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success<AccountAvatarDto>
            (MessagesList.AccountUploadAvatarSuccess.GetMessage().Code,
            MessagesList.AccountUploadAvatarSuccess.GetMessage().Message,
            new AccountAvatarDto
            {
                CropAvatarLink = uploadImages[0].ImageUrl,
                FullAvatarLink = uploadImages[1].ImageUrl,
            }));
    }
}
