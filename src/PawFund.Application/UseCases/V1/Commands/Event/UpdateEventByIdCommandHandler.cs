using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Events;
using PawFund.Contract.Shared;
using PawFund.Contract.Abstractions.Message;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Event
{
    public sealed class UpdateEventByIdCommandHandler : ICommandHandler<Command.UpdateEventCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Branch, Guid> _branchRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventByIdCommandHandler(IRepositoryBase<Branch, Guid> branchRepository, IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IUnitOfWork unitOfWork)
        {
            _branchRepository = branchRepository;
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(Command.UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FindByIdAsync(request.BranchId);
            var existEvent = await _eventRepository.FindByIdAsync(request.Id);
            if (existEvent == null)
            {
                throw new EventException.EventNotFoundException("This event is not exist, please choose other event");
            }
            else if (branch == null)
            {
                throw new BranchException.BranchNotFoundException("This branch is not exist, please choose other branch");  
            }
            existEvent.Name = request.Name;
            existEvent.StartDate = request.StartDate;
            existEvent.EndDate = request.EndDate;
            existEvent.Description = request.Description;
            existEvent.MaxAttendees = request.MaxAttendees;
            existEvent.BranchId = request.BranchId != null ? request.BranchId : existEvent.BranchId;
            _eventRepository.Update(existEvent);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Update Event Success");
        }
    }
}
