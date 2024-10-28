using AutoMapper;
using Microsoft.Extensions.Options;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.ChatHistoryDTOs;
using PawFund.Contract.Services.Messages;
using PawFund.Contract.Settings;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Queries.Message;

public sealed class GetListUserNeedSupportQueryHandler : IQueryHandler<Query.GetListUserNeedSupportQuery, Success<List<ChatHistoryDto>>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly AccountStaffAssistantSetting _accountStaffAssistantSetting;
    private readonly IMapper _mapper;
    public GetListUserNeedSupportQueryHandler
        (IDPUnitOfWork dpUnitOfWork,
        IOptions<AccountStaffAssistantSetting> accountStaffAssistantConfig,
        IMapper mapper)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _accountStaffAssistantSetting = accountStaffAssistantConfig.Value;
        _mapper = mapper;
    }

    public async Task<Result<Success<List<ChatHistoryDto>>>> Handle(Query.GetListUserNeedSupportQuery request, CancellationToken cancellationToken)
    {
        var staff = await _dpUnitOfWork.AccountRepositories.GetByEmailAsync(_accountStaffAssistantSetting.Email);
        var result = await _dpUnitOfWork.ChatHistoryRepository.GetUserNeedSupportAsync(staff.Id);
        var chatHistoryDtos = _mapper.Map<List<ChatHistoryDto>>(result);
        return Result.Success(new Success<List<ChatHistoryDto>>("", "", chatHistoryDtos));
    }
}
