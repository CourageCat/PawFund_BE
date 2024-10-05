using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Application.UseCases.V1.Queries.User
{
    public sealed class GetUserQueryHandler : IQueryHandler<Query.GetUserById, Response.UserResponse>
    {

        private readonly IRepositoryBase<Account, Guid> _userRepository;

        public GetUserQueryHandler(IRepositoryBase<Account, Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Response.UserResponse>> Handle(Query.GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.Id) ?? throw new UserException.UserNotFoundException(request.Id);

            var result = new Response.UserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.Status);
            return Result.Success(result);
        }

        //public async Task<Result<Response.UserResponse>> GetListUser(Query.GetUserById request, CancellationToken cancellationToken)
        //{
        //    var user = await _userRepository.FindByIdAsync(request.Id) ?? throw new UserException.UserNotFoundException(request.Id);

        //    var result = new Response.UserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.Status);
        //    return Result.Success(result);
        //}
    }
}
