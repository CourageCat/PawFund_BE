
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Events;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Event;

public sealed class CreateEventCommandHandler : ICommandHandler<Command.CreateEventCommand>
{
    private readonly IRepositoryBase<PawFund.Domain.Entities.Branch, Guid> _branchRepository;
    private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEventCommandHandler(IRepositoryBase<Branch, Guid> branchRepository, IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.CreateEventCommand request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.FindByIdAsync(request.BranchId);
        if (branch != null)
        {
            var newEvent = new Domain.Entities.Event()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Description = request.Description,
                MaxAttendees = request.MaxAttendees,
                BranchId = request.BranchId,
            };
            _eventRepository.Add(newEvent);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success("Create Event Success");
        }
        else 
        {
            throw new BranchException.BranchNotFoundException("This branch is not exist, please choose other branch");
        }
    }
}
