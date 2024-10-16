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
using PawFund.Contract.Abstractions.Shared;
using static PawFund.Contract.Services.Accounts.Response;
using static PawFund.Contract.Services.Products.Response;
using AutoMapper;
using PawFund.Domain.Abstractions.Dappers.Repositories;

namespace PawFund.Application.UseCases.V1.Queries.User
{
    public sealed class GetUsersQueryHandler : IQueryHandler<Query.GetUsersQueryHandler, PagedResult<UsersResponse>>
    {
        private readonly IDPUnitOfWork _dPUnitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;


        public GetUsersQueryHandler(IDPUnitOfWork dPUnitOfWork, IMapper mapper, IAccountRepository accountRepository)
        {
            _dPUnitOfWork = dPUnitOfWork;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<Result<PagedResult<UsersResponse>>> Handle(Query.GetUsersQueryHandler request, CancellationToken cancellationToken)
        {
            List<GetUserAccount.AccountDto> listAccountDto = new List<GetUserAccount.AccountDto>();
            var listUser = await _dPUnitOfWork.AccountRepositories.GetListUser() ?? throw new UserException.ListUserNotFoundException();

            //if(listUser != null)
            //{
            //    foreach (var account in listUser)
            //    {
            //        listAccountDto.Add(new GetUserAccount.AccountDto
            //        {
            //            Id = account.Id,
            //            FirstName = account.FirstName,
            //            LastName = account.LastName,
            //            Email = account.Email,
            //            PhoneNumber = account.PhoneNumber,
            //        });
            //    }
            //}

            //var result = new Response.GetListUserResponse(listAccountDto);
            //var result = new PagedResult<GetListUserResponse> { listAccountDto };
            //return Result.Success(result);

            var accountPagedResult = await _accountRepository.GetPagedAsync(request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);
            var result = _mapper.Map<PagedResult<UsersResponse>>(accountPagedResult);
            return Result.Success(result);
        }
    }
}
