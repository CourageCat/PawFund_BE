
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

namespace PawFund.Application.UseCases.V1.Commands.Event;

public sealed class CreateEventCommandHandler : ICommandHandler<Command.CreateEventCommand>
{
    private readonly IRepositoryBase<PawFund.Domain.Entities.Branch, Guid> _branchRepository;
    private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
    private readonly IEFUnitOfWork _efUnitOfWork;
    private readonly IDPUnitOfWork _dPUnitOfWork;

    public CreateEventCommandHandler(IRepositoryBase<Domain.Entities.Branch, Guid> branchRepository, IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dPUnitOfWork)
    {
        _branchRepository = branchRepository;
        _eventRepository = eventRepository;
        _efUnitOfWork = efUnitOfWork;
        _dPUnitOfWork = dPUnitOfWork;
    }

    public async Task<Result> Handle(Command.CreateEventCommand request, CancellationToken cancellationToken)
    {
        //check branch for event
        var branch = await _dPUnitOfWork.BranchRepositories.GetByIdAsync(request.BranchId);
        if (branch != null || branch.IsDeleted != true)
        {
            //create new event
            var newEvent = Domain.Entities.Event.CreateEvent(request.Name, request.StartDate, request.EndDate, request.Description, request.MaxAttendees, request.BranchId, DateTime.Now, DateTime.Now, false);
            newEvent.Status = Contract.Enumarations.Event.EventStatus.NotApproved;
            _eventRepository.Add(newEvent);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);

            //Return result
            return Result.Success(new Success(MessagesList.CreateEventSuccessfully.GetMessage().Code, MessagesList.CreateEventSuccessfully.GetMessage().Message));
        }
        else
        {
            throw new BranchException.BranchNotFoundException(request.BranchId);
        }
    }
}
