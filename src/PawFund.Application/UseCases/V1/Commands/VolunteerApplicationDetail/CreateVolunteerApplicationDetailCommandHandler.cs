using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Contract.Shared;
using PawFund.Domain.Exceptions;
using PawFund.Contract.Enumarations.VolunteerApplication;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Commands.VolunteerApplicationDetail
{
    public sealed class CreateVolunteerApplicationDetailCommandHandler : ICommandHandler<Command.CreateVolunteerApplicationDetailCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.VolunteerApplicationDetail, Guid> _volunteerApplicationDetailRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IDPUnitOfWork _dpUnitOfWork;

        public CreateVolunteerApplicationDetailCommandHandler(IRepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid> volunteerApplicationDetailRepository, IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork, IDPUnitOfWork dpUnitOfWork)
        {
            _volunteerApplicationDetailRepository = volunteerApplicationDetailRepository;
            _eventRepository = eventRepository;
            _efUnitOfWork = efUnitOfWork;
            _dpUnitOfWork = dpUnitOfWork;
        }

        public async Task<Result> Handle(Command.CreateVolunteerApplicationDetailCommand request, CancellationToken cancellationToken)
        {
            //check if event exist
            var existEvent = await _eventRepository.FindByIdAsync(request.eventId, cancellationToken);
            if (existEvent == null)
            {
                throw new EventException.EventNotFoundException(request.eventId);
            }

            //check if volunteer already regist to this event
            var existApplication = await _dpUnitOfWork.VolunteerApplicationDetailRepository.CheckVolunteerApplicationExists(request.eventId, request.userId);
            if (existApplication)
            {
                throw new VolunteerApplicationException.VolunteerApplicationAlreadyRegistException();
            }

            List<string> listActivity = request.listActivity;

            if (listActivity.Count > 2)
            {
                throw new VolunteerApplicationException.VolunteerApplicationMaximumException();
            }

            foreach (var item in listActivity)
            {
                var newVolunteerApplication = Domain.Entities.VolunteerApplicationDetail.createVolunteerApplication(VolunteerApplicationStatus.Pending, request.description, null, Guid.Parse(item), request.eventId, request.userId, DateTime.Now, DateTime.Now, false);
                _volunteerApplicationDetailRepository.Add(newVolunteerApplication);
                await _efUnitOfWork.SaveChangesAsync();
            }

            return Result.Success(new Success(MessagesList.CreateVolunteerApplicationSuccessfully.GetMessage().Code, MessagesList.CreateVolunteerApplicationSuccessfully.GetMessage().Message));

        }
    }
}
