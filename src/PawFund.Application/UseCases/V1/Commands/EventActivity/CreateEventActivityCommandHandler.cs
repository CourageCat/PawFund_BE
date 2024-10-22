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
    public sealed class CreateEventActivityCommandHandler : ICommandHandler<Command.CreateEventActivityCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.EventActivity, Guid> _eventActivityRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public CreateEventActivityCommandHandler(IRepositoryBase<Domain.Entities.EventActivity, Guid> eventActivityRepository, IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dPUnitOfWork)
        {
            _eventActivityRepository = eventActivityRepository;
            _eventRepository = eventRepository;
            _efUnitOfWork = efUnitOfWork;
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result> Handle(Command.CreateEventActivityCommand request, CancellationToken cancellationToken)
        {
            var existEvent = await _dPUnitOfWork.EventRepository.GetByIdAsync(request.EventId);
            if (existEvent == null || existEvent.IsDeleted == true)
            {
                throw new EventException.EventNotFoundException(request.EventId);
            }
            else
            {
                var newActivityEvent = Domain.Entities.EventActivity.CreateEventActivity(request.Name, request.Quantity, request.StartDate, request.Description, true, request.EventId, DateTime.Now, DateTime.Now, false);
                _eventActivityRepository.Add(newActivityEvent);
                await _efUnitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Success(new Success(MessagesList.CreateEventActivitySuccessfully.GetMessage().Code, MessagesList.CreateEventActivitySuccessfully.GetMessage().Message));
            }
        }
    }
}
