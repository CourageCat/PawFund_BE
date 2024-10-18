using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Abstractions.Services;

namespace PawFund.Application.UseCases.V1.Commands.Cat
{
    public sealed class CreateCatCommandHandler : ICommandHandler<Command.CreateCatCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
        private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IMediaService _mediaService;

        public CreateCatCommandHandler(IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IEFUnitOfWork efUnitOfWork, IMediaService mediaService)
        {
            _catRepository = catRepository;
            _branchRepository = branchRepository;
            _efUnitOfWork = efUnitOfWork;
            _mediaService = mediaService;
        }

        public async Task<Result> Handle(Command.CreateCatCommand request, CancellationToken cancellationToken)
        {
            var branchFound = await _branchRepository.FindByIdAsync(request.BranchId);
            if (branchFound == null)
                throw new BranchException.BranchNotFoundException(request.BranchId);

            //_mediaService.UploadImage()

            var catCreated = Domain.Entities.Cat.CreateCat(request.Sex, request.Name, request.Age, request.Breed, request.Size, request.Color, request.Description, request.BranchId, DateTime.Now, DateTime.Now, false);
            _catRepository.Add(catCreated);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Create Cat Successfully.");

        }
    }
}
