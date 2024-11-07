using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Commands.Event
{
    public sealed class RejectEventByAdminCommandHandler : ICommandHandler<Command.RejectedEventByAdmin>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public RejectEventByAdminCommandHandler(IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork)
        {
            _eventRepository = eventRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<Result> Handle(Command.RejectedEventByAdmin request, CancellationToken cancellationToken)
        {
            //find event
            var existEvent = await _eventRepository.FindByIdAsync(request.Id);

            if (existEvent == null)
            {
                throw new EventException.EventNotFoundException(request.Id);
            }

            existEvent.Status = Contract.Enumarations.Event.EventStatus.Rejected;
            existEvent.IsDeleted = true;
            _eventRepository.Update(existEvent);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new Success(MessagesList.RejectEventSuccessfully.GetMessage().Code, MessagesList.RejectEventSuccessfully.GetMessage().Message));
        
        }
    }
}
