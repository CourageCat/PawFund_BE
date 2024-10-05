using PawFund.Contract.Abstractions.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Exceptions;
using System.Xml;
using PawFund.Contract.DTOs.Account;

namespace PawFund.Application.UseCases.V1.Queries.User
{
    public sealed class GetListUserQueryHandler : IQueryHandler<Query.GetListUserQuery, Response.GetListUser>
    {
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public GetListUserQueryHandler(IDPUnitOfWork dPUnitOfWork)
        {
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result<Response.GetListUser>> Handle(Query.GetListUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _dPUnitOfWork.AccountRepositories.GetListUser() ?? throw new UserException.ListUserNotFoundException();

            var listUserDto = new GetUserAccount.ListAccountDto()
            {
                listAccount = user.Select(account => new GetUserAccount.AccountDto
                {
                    Id = account.Id,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                   
                }).ToList()
            };

            var result = new Response.GetListUser(listUserDto);
            return Result.Success(result);
        }
    }
}
