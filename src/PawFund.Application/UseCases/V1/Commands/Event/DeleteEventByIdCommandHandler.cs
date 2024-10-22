
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;
using PawFund.Persistence;

namespace PawFund.Application.UseCases.V1.Commands.Event
{
    public sealed class DeleteEventByIdCommandHandler : ICommandHandler<Command.DeleteEventCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public DeleteEventByIdCommandHandler(IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dPUnitOfWork)
        {
            _eventRepository = eventRepository;
            _efUnitOfWork = efUnitOfWork;
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var existEvent = await _eventRepository.FindByIdAsync(request.Id);
            if (existEvent == null)
            {
                throw new EventException.EventNotFoundException(request.Id);
            }
            existEvent.UpdateEvent(existEvent.Name, existEvent.StartDate, existEvent.EndDate, existEvent.Description, existEvent.MaxAttendees, existEvent.BranchId, true);
            _eventRepository.Update(existEvent);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new Success(MessagesList.DeleteEventSuccessfully.GetMessage().Code, MessagesList.DeleteEventSuccessfully.GetMessage().Message));
        }
    }
}
