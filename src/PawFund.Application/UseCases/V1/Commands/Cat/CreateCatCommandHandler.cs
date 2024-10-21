using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Abstractions.Services;
using PawFund.Domain.Entities;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.Cat
{
    public sealed class CreateCatCommandHandler : ICommandHandler<Command.CreateCatCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
        private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
        private readonly IRepositoryBase<Domain.Entities.ImageCat, Guid> _imageCatRepository;

        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IMediaService _mediaService;

        public CreateCatCommandHandler(IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IEFUnitOfWork efUnitOfWork, IMediaService mediaService, IRepositoryBase<ImageCat, Guid> imageCatRepository)
        {
            _catRepository = catRepository;
            _branchRepository = branchRepository;
            _efUnitOfWork = efUnitOfWork;
            _mediaService = mediaService;
            _imageCatRepository = imageCatRepository;
        }

        public async Task<Result> Handle(Command.CreateCatCommand request, CancellationToken cancellationToken)
        {
            var branchFound = await _branchRepository.FindByIdAsync(request.BranchId);
            if (branchFound == null)
                throw new BranchException.BranchNotFoundException(request.BranchId);

            var uploadImages = await _mediaService.UploadImages(request.Images);

            var cat = Domain.Entities.Cat.CreateCat(
                request.Sex,
                request.Name,
                request.Age,
                request.Breed,
                request.Weight,
                request.Color,
                request.Description,
                request.BranchId);

            _catRepository.Add(cat);

            var imageCats = uploadImages.Select(image => new ImageCat
            {
                ImageUrl = image.ImageUrl,
                PublicImageId = image.PublicImageId,
                CatId = cat.Id
            }).ToList();

            _imageCatRepository.AddRange(imageCats);

            await _efUnitOfWork.SaveChangesAsync();

            return Result.Success(new Success(MessagesList.CreateCatSuccessfully.GetMessage().Code,
                MessagesList.CreateCatSuccessfully.GetMessage().Message));
        }
    }
}
