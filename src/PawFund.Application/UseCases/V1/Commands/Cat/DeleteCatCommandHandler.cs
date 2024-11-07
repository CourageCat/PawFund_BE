using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.Cat;
    public sealed class DeleteCatCommandHandler : ICommandHandler<Command.DeleteCatCommand> 
    {
        private readonly IRepositoryBase<Domain.Entities.Cat, Guid> _catRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public DeleteCatCommandHandler(IRepositoryBase<Domain.Entities.Cat, Guid> catRepository, IEFUnitOfWork efUnitOfWork)
        {
            _catRepository = catRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var catFound = await _catRepository.FindByIdAsync(request.Id);
            if (catFound == null || catFound.IsDeleted == true)
            {
                throw new CatException.CatNotFoundException();
            }
            catFound.DeleteCat();
            _catRepository.Update(catFound);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new Success(MessagesList.DeleteCatSuccessfully.GetMessage().Code,
            MessagesList.DeleteCatSuccessfully.GetMessage().Message));
        }
    }

