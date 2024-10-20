using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Services.Event;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Commands.Event
{
    public sealed class UpdateEventByIdCommandHandler : ICommandHandler<Command.UpdateEventCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Branch, Guid> _branchRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public UpdateEventByIdCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dPUnitOfWork)
        {
            _branchRepository = branchRepository;
            _eventRepository = eventRepository;
            _efUnitOfWork = efUnitOfWork;
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result> Handle(Command.UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FindByIdAsync(request.BranchId);
            var existEvent = await _eventRepository.FindByIdAsync(request.Id);
            if (existEvent == null)
            {
                throw new EventException.EventNotFoundException(request.Id);
            }
            else if (branch == null)
            {
                throw new BranchException.BranchNotFoundException(request.BranchId);
            }

            existEvent.UpdateEvent(request.Name, request.StartDate, request.EndDate, request.Description, request.MaxAttendees, request.BranchId, false);
            existEvent.Status = Contract.Enumarations.Event.EventStatus.NotApproved;
            _eventRepository.Update(existEvent);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Update Event Success");
        }
    }
}
