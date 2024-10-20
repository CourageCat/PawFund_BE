using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Contract.Shared;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Enumarations.VolunteerApplication;

namespace PawFund.Application.UseCases.V1.Commands.VolunteerApplicationDetail
{
    public sealed class CreateVolunteerApplicationDetailCommandHandler : ICommandHandler<Command.CreateVolunteerApplicationDetailCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.VolunteerApplicationDetail, Guid> _volunteerApplicationDetailRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public CreateVolunteerApplicationDetailCommandHandler(IRepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid> volunteerApplicationDetailRepository, IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork)
        {
            _volunteerApplicationDetailRepository = volunteerApplicationDetailRepository;
            _eventRepository = eventRepository;
            _efUnitOfWork = efUnitOfWork;
        }

        public async Task<Result> Handle(Command.CreateVolunteerApplicationDetailCommand request, CancellationToken cancellationToken)
        {
            //check if event exist
            var existEvent = await _eventRepository.FindByIdAsync(request.form.eventId, cancellationToken);
            if (existEvent == null)
            {
                throw new EventException.EventNotFoundException(request.form.eventId);
            }
            
            var listActivity = request.form.listActivity.Split(',');
            foreach (var item in listActivity)
            {
                var newVolunteerApplication = Domain.Entities.VolunteerApplicationDetail.createVolunteerApplication(VolunteerApplicationStatus.Pending, request.form.description, null, Guid.Parse(item), request.form.eventId, request.userId, DateTime.Now, DateTime.Now, false);
                _volunteerApplicationDetailRepository.Add(newVolunteerApplication);
                await _efUnitOfWork.SaveChangesAsync();
            }

            return Result.Success("Register to be volunteer success");

        }
    }
}
