
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Abstractions;
using PawFund.Contract.Enumarations.VolunteerApplication;
using MediatR;
using PawFund.Domain.Entities;
using PawFund.Domain.Abstractions.Dappers.Repositories;

namespace PawFund.Application.UseCases.V1.Commands.VolunteerApplicationDetail
{
    public sealed class ApproveVolunteerApplicationCommandHandler : ICommandHandler<Command.ApproveVolunteerApplicationCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.VolunteerApplicationDetail, Guid> _volunteerApplicationDetailRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.Account, Guid> _accountRepository;
        private readonly IRepositoryBase<PawFund.Domain.Entities.EventActivity, Guid> _eventActivityRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;
        private readonly IPublisher _publisher;

        public ApproveVolunteerApplicationCommandHandler(IRepositoryBase<Domain.Entities.VolunteerApplicationDetail, Guid> volunteerApplicationDetailRepository, IEventActivityRepository dpEventActivityRepository, IRepositoryBase<Domain.Entities.Account, Guid> accountRepository, IRepositoryBase<Domain.Entities.EventActivity, Guid> eventActivityRepository, IEFUnitOfWork efUnitOfWork, IPublisher publisher)
        {
            _volunteerApplicationDetailRepository = volunteerApplicationDetailRepository;
            _accountRepository = accountRepository;
            _eventActivityRepository = eventActivityRepository;
            _efUnitOfWork = efUnitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(Command.ApproveVolunteerApplicationCommand request, CancellationToken cancellationToken)
        {
            // Tìm kiếm hồ sơ tình nguyện viên
            var existVolunteerApplication = await _volunteerApplicationDetailRepository.FindByIdAsync(request.detailId);

            // Lấy thông tin hoạt động sự kiện liên quan
            var eventActivity = await _eventActivityRepository.FindByIdAsync(existVolunteerApplication.EventActivityId);

            // Kiểm tra số lượng tình nguyện viên có đủ hay không
            if (eventActivity.NumberOfVolunteer >= eventActivity.Quantity)
            {
                throw new InvalidOperationException("Event activity has reached the maximum number of volunteers.");
            }

            //change status application
            existVolunteerApplication.UpdateVolunteerApplication(VolunteerApplicationStatus.Approved, null);
            await _efUnitOfWork.SaveChangesAsync();

            // Cập nhật số lượng tình nguyện viên
            eventActivity.NumberOfVolunteer += 1;
            _eventActivityRepository.Update(eventActivity);
            await _efUnitOfWork.SaveChangesAsync(); // Lưu thay đổi ngay sau khi cập nhật số lượng

            // Lấy thông tin tài khoản liên quan
            var account = await _accountRepository.FindByIdAsync(existVolunteerApplication.AccountId);

            // Gửi email thông báo
            await _publisher.Publish(
                new DomainEvent.ApproveSendMail(Guid.NewGuid(), account.Email, eventActivity.Name),
                cancellationToken
            );

            return Result.Success("Approve Application Success");
        }

    }
}
