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
    public sealed class GetListUserQueryHandler : IQueryHandler<Query.GetListUserQuery, Response.GetListUserResponse>
    {
        private readonly IDPUnitOfWork _dPUnitOfWork;

        public GetListUserQueryHandler(IDPUnitOfWork dPUnitOfWork)
        {
            _dPUnitOfWork = dPUnitOfWork;
        }

        public async Task<Result<Response.GetListUserResponse>> Handle(Query.GetListUserQuery request, CancellationToken cancellationToken)
        {
            List<GetUserAccount.AccountDto> listAccountDto = new List<GetUserAccount.AccountDto>();
            var listUser = await _dPUnitOfWork.AccountRepositories.GetListUser() ?? throw new UserException.ListUserNotFoundException();

            if(listUser != null)
            {
                foreach (var account in listUser)
                {
                    listAccountDto.Add(new GetUserAccount.AccountDto
                    {
                        Id = account.Id,
                        FirstName = account.FirstName,
                        LastName = account.LastName,
                        Email = account.Email,
                        PhoneNumber = account.PhoneNumber,
                    });
                }
            }

            var result = new Response.GetListUserResponse(listAccountDto);
            return Result.Success(result);
        }
    }
}
