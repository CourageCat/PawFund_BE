
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.EventActivities;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.EventActivity
{
    public sealed class CreateEventActivityCommandHandler : ICommandHandler<Command.CreateEventActivityCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.EventActivity, Guid> _eventActivityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventActivityCommandHandler(IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IRepositoryBase<Domain.Entities.EventActivity, Guid> eventActivityRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _eventActivityRepository = eventActivityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(Command.CreateEventActivityCommand request, CancellationToken cancellationToken)
        {
            var existEvent = await _eventRepository.FindByIdAsync(request.EventId);
            if (existEvent != null)
            {
                var eventActivity = new Domain.Entities.EventActivity()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    StartDate = request.Time,
                    Description = request.Desciption,
                    EventId = request.EventId,
                    Quantity = request.NumberOfVolunteer  
                };
                _eventActivityRepository.Add(eventActivity);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success("Create Event Activity Success");
            }
            else
            {
                throw new BranchException.BranchNotFoundException("This event is not exist, please choose other branch");
            }
        }
    }
}
