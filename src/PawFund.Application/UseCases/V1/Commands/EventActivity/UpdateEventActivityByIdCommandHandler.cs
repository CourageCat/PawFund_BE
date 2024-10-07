using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.EventActivity;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.EventActivity
{
    public sealed class UpdateEventActivityByIdCommandHandler : ICommandHandler<Command.UpdateEventActivityCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.EventActivity, Guid> _eventActivityRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public UpdateEventActivityByIdCommandHandler(IRepositoryBase<Domain.Entities.EventActivity, Guid> eventActivityRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dPUnitOfWork)
        {
            _eventActivityRepository = eventActivityRepository;
            _efUnitOfWork = efUnitOfWork;
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result> Handle(Command.UpdateEventActivityCommand request, CancellationToken cancellationToken)
        {
            var existEvent = await _dPUnitOfWork.EventRepository.GetByIdAsync(request.EventId);
            var existActivity = await _dPUnitOfWork.EventActivityRepositories.GetByIdAsync(request.Id);
            if (existActivity == null)
            {
                throw new EventActivityException.EventActivityNotFoundException(request.Id);
            }
            else if (existEvent == null)
            {
                throw new EventException.EventNotFoundException(request.EventId);
            }

            existActivity.UpdateEventActivity(request.Name, request.Quantity, request.StartDate, request.Description, request.Status, request.EventId, false);
            _eventActivityRepository.Update(existActivity);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("Update Event Activity Success");
        }
    }
}
