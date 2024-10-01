using Microsoft.EntityFrameworkCore.Update.Internal;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Admins;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;


namespace PawFund.Application.UseCases.V1.Commands.Admin
{
    public sealed class BanUserCommandHandler : ICommandHandler<Command.BanUserCommand>
    {
        private readonly IRepositoryBase<PawFund.Domain.Entities.Account, Guid> _adminRepository;
        private readonly IEFUnitOfWork _unitOfWork;

        public BanUserCommandHandler(IEFUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(Command.BanUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _adminRepository.FindByIdAsync(request.Id);
            if(user == null)
            {
                return Result.Failure(Error.NullValue);
            }

            user.Status = false;

            _adminRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();


            return Result.Success("Hello");
        }
    }
}
