using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.EventActivity;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.EventActivity
{
    public sealed class DeleteEventActivityByIdCommandHandler : ICommandHandler<Command.DeleteEventActivityCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.EventActivity, Guid> _eventActivityRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public DeleteEventActivityByIdCommandHandler(IRepositoryBase<Domain.Entities.EventActivity, Guid> eventActivityRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dPUnitOfWork)
        {
            _eventActivityRepository = eventActivityRepository;
            _efUnitOfWork = efUnitOfWork;
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteEventActivityCommand request, CancellationToken cancellationToken)
        {
            var existActivity = await _eventActivityRepository.FindByIdAsync(request.Id);
            if (existActivity == null)
            {
                throw new EventActivityException.EventActivityNotFoundException(request.Id);
            }
            existActivity.UpdateEventActivity(existActivity.Name, existActivity.Quantity, existActivity.StartDate, existActivity.Description, existActivity.Status, existActivity.EventId, true);
            _eventActivityRepository.Update(existActivity);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new Success(MessagesList.DeleteEventActivitySuccessfully.GetMessage().Code, MessagesList.DeleteEventActivitySuccessfully.GetMessage().Message));
        }
    }
}
