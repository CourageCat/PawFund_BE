using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Contract.Abstractions.Services;
using PawFund.Domain.Entities;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.Cat;

public sealed class CreateCatCommandHandler : ICommandHandler<Command.CreateCatCommand>
{
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IMediaService _mediaService;

    public CreateCatCommandHandler(IEFUnitOfWork efUnitOfWork, IMediaService mediaService)
    {
        _efUnitOfWork = efUnitOfWork;
        _mediaService = mediaService;
    }

    public async Task<Result> Handle(Command.CreateCatCommand request, CancellationToken cancellationToken)
    {
        var account = await _efUnitOfWork.AccountRepository.FindByIdAsync((Guid)request.UserId);

        var uploadImages = await _mediaService.UploadImagesAsync(request.Images);

        var accountBranchId = account.Branches.FirstOrDefault().Id;

        var cat = Domain.Entities.Cat.CreateCat(
            request.Sex,
            request.Name,
            request.Age,
            request.Breed,
            request.Weight,
            request.Color,
            request.Description,
            accountBranchId,
            request.Sterilization);

        _efUnitOfWork.CatRepository.Add(cat);

        var imageCats = uploadImages.Select(image => new ImageCat
        {
            ImageUrl = image.ImageUrl,
            PublicImageId = image.PublicImageId,
            CatId = cat.Id
        }).ToList();

        _efUnitOfWork.ImageCatRepository.AddRange(imageCats);

        await _efUnitOfWork.SaveChangesAsync();

        return Result.Success(new Success(MessagesList.CreateCatSuccessfully.GetMessage().Code,
            MessagesList.CreateCatSuccessfully.GetMessage().Message));
    }
}
