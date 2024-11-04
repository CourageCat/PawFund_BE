using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.Accounts;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Exceptions;
using PawFund.Contract.DTOs.Account;
using PawFund.Contract.Abstractions.Shared;
using static PawFund.Contract.Services.Accounts.Response;
using AutoMapper;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using MediatR;
using PawFund.Contract.Enumarations.MessagesList;

namespace PawFund.Application.UseCases.V1.Queries.User
{
    public sealed class GetUsersQueryHandler : IQueryHandler<Query.GetUsersQueryHandler, Success<PagedResult<UsersResponse>>>
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

        public async Task<Result<Success<PagedResult<UsersResponse>>>>Handle(Query.GetUsersQueryHandler request, CancellationToken cancellationToken)
        {
            //List<AccountDto> listAccountDto = new List<AccountDto>();
            //var listUser = await _dPUnitOfWork.AccountRepositories.GetListUserAsync() ?? throw new UserException.ListUserNotFoundException();

            var accountPagedResult = await _accountRepository.GetUsersPagedAsync(request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);
            var result = _mapper.Map<PagedResult<UsersResponse>>(accountPagedResult);
            if (result.Items.Count == 0)
            {
                return Result.Success(new Success<PagedResult<Response.UsersResponse>>(MessagesList.UserEmptyUsersException.GetMessage().Code, MessagesList.UserEmptyUsersException.GetMessage().Message, result));

            }
            return Result.Success(new Success<PagedResult<Response.UsersResponse>>(MessagesList.GetUsersSuccess.GetMessage().Code, MessagesList.GetUsersSuccess.GetMessage().Message, result));
        }
    }
}
