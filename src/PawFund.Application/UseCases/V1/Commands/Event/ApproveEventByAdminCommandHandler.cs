using Azure.Core;
using Microsoft.Extensions.Logging;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.Event;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Event;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Commands.Event
{
    public sealed class ApproveEventByAdminCommandHandler : ICommandHandler<Command.ApprovedEventByAdmin>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Event, Guid> _eventRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IBackgroundJobService _backgroundJobService;

        public ApproveEventByAdminCommandHandler(IRepositoryBase<Domain.Entities.Event, Guid> eventRepository, IEFUnitOfWork efUnitOfWork, IBackgroundJobService backgroundJobService)
        {
            _eventRepository = eventRepository;
            _efUnitOfWork = efUnitOfWork;
            _backgroundJobService = backgroundJobService;
        }

        public async Task<Result> Handle(Command.ApprovedEventByAdmin request, CancellationToken cancellationToken)
        {
            //find event
            var existEvent = await _eventRepository.FindByIdAsync(request.Id);

            if (existEvent == null)
            {
                throw new EventException.EventNotFoundException(request.Id);
            }

            existEvent.Status = EventStatus.NotStarted;
            _eventRepository.Update(existEvent);
            await _efUnitOfWork.SaveChangesAsync();

            // Lên lịch background job để kiểm tra trạng thái sự kiện
            _backgroundJobService.ScheduleRecurringJob(
                $"CheckEventStatus-{request.Id}",
                () => CheckAndUpdateEventStatusAsync(request.Id),
                CronExpressionsEvent.Hourly); // Sử dụng biểu thức cron từ lớp tiện ích

            return Result.Success(new Success(MessagesList.ApproveEventSuccessfully.GetMessage().Code, MessagesList.ApproveEventSuccessfully.GetMessage().Message));
        }

        // Phương thức kiểm tra và cập nhật trạng thái sự kiện
        public async Task CheckAndUpdateEventStatusAsync(Guid eventId)
        {
            var eventEntity = await _eventRepository.FindByIdAsync(eventId);
            if (eventEntity != null && eventEntity.Status == EventStatus.NotStarted && eventEntity.StartDate <= DateTime.Now)
            {
                eventEntity.Status = EventStatus.Ongoing;
                _eventRepository.Update(eventEntity);
                await _efUnitOfWork.SaveChangesAsync();
            }
        }
    }
}
