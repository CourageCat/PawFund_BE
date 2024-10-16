using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Queries.User
{
    public sealed class GetUserQueryHandler : IQueryHandler<Query.GetUserByIdQuery, Response.UserResponse>
    {

        //private readonly IRepositoryBase<Account, Guid> _userRepository;
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public GetUserQueryHandler(IDPUnitOfWork dPUnitOfWork)
        {
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result<Response.UserResponse>> Handle(Query.GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dPUnitOfWork.AccountRepositories.GetByIdAsync(request.Id) ?? throw new UserException.UserNotFoundException(request.Id);

            var result = new Response.UserResponse(user.Id, user.FirstName, user.LastName , user.Email, user.PhoneNumber, user.Status );
            return Result.Success(result);
        }
    }
}
