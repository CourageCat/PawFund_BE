using Microsoft.EntityFrameworkCore.Update.Internal;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Admins;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;


namespace PawFund.Application.UseCases.V1.Commands.Admin
{
    public sealed class ChangeUserStatusCommandHandler : ICommandHandler<Command.ChangeUserStatusCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Account, Guid> _adminRepository;
        private readonly IEFUnitOfWork _unitOfWork;

        public ChangeUserStatusCommandHandler(IEFUnitOfWork unitOfWork, IRepositoryBase<PawFund.Domain.Entities.Account, Guid> adminRepository)
        {
            _unitOfWork = unitOfWork;
            _adminRepository = adminRepository;
        }

        public async Task<Result> Handle(Command.ChangeUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _adminRepository.FindByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                return Result.Failure(Error.NullValue);
            }

            user.Status = !user.Status;

            _adminRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Status == false ? Result.Success("UnBan user successfully. ") : Result.Success("Ban User Successfully");
        }
    }
}
