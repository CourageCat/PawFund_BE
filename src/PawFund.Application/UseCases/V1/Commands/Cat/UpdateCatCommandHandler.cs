using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Abstractions.Services;
using PawFund.Domain.Entities;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Enumarations.Cat;

namespace PawFund.Application.UseCases.V1.Commands.Cat;
public sealed class UpdateCatCommandHandler : ICommandHandler<Command.UpdateCatCommand, Success>
{
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IMediaService _mediaService;

    public UpdateCatCommandHandler
        (IEFUnitOfWork efUnitOfWork,
        IMediaService mediaService)
    {
        _efUnitOfWork = efUnitOfWork;
        _mediaService = mediaService;
    }

    public async Task<Result<Success>> Handle(Command.UpdateCatCommand request, CancellationToken cancellationToken)
    {
        var catFound = await _efUnitOfWork.CatRepository.FindByIdAsync(request.CatId);
        if (catFound == null || catFound.IsDeleted == true)
            throw new CatException.CatNotFoundException();

        if (request.OldImages != null && request.OldImages.Count > 0)
        {
            await _efUnitOfWork.ImageCatRepository.DeleteImageCatAsync(request.OldImages, catFound.Id);
        }

        if (request.NewImages != null && request.NewImages.Count > 0)
        {
            var uploadImages = await _mediaService.UploadImagesAsync(request.NewImages);
            var imageCats = uploadImages.Select(image => new ImageCat
            {
                ImageUrl = image.ImageUrl,
                PublicImageId = image.PublicImageId,
                CatId = catFound.Id
            }).ToList();

            _efUnitOfWork.ImageCatRepository.AddRange(imageCats);
        }

        catFound.UpdateCat((CatSex)request.Sex, request.Name, request.Age, request.Breed, (decimal)request.Weight, request.Color, request.Description, (bool)request.Sterilization);

        _efUnitOfWork.CatRepository.Update(catFound);

        await _efUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new Success(MessagesList.UpdateCatSuccessFully.GetMessage().Code, MessagesList.UpdateCatSuccessFully.GetMessage().Message));
    }
}

