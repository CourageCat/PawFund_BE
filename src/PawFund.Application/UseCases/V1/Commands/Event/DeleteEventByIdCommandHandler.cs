
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Events;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Event
{
    public sealed class DeleteEventByIdCommandHandler : ICommandHandler<Command.DeleteEventCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventByIdCommandHandler(IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var existEvent = await _eventRepository.FindByIdAsync(request.Id);
            if (existEvent == null) 
            {
                throw new EventException.EventNotFoundException("This event is not exist, please choose other event");
            }
            else if (existEvent.IsDeleted == true)
            {
                throw new EventException.EventIsDeletedException("This event is already delete");
            }
            existEvent.IsDeleted = true;
            return Result.Success("Delete Event Success");
        }
    }
}
