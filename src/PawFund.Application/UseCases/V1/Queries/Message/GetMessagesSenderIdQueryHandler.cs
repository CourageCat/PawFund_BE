using AutoMapper;
using Microsoft.Extensions.Options;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.MessageDTOs;
using PawFund.Contract.Services.Messages;
using PawFund.Contract.Settings;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;

namespace PawFund.Application.UseCases.V1.Queries.Message;

public sealed class GetMessagesSenderIdQueryHandler : IQueryHandler<Query.GetMessagesSenderIdQuery, Success<List<CreateMessageDto>>>
{
    private readonly AccountStaffAssistantSetting _accountStaffAssistantSetting;
    private readonly IDPUnitOfWork _dpUnitOfWork;
    private readonly IMapper _mapper;

    public GetMessagesSenderIdQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper, 
        IOptions<AccountStaffAssistantSetting> accountStaffAssistantConfig)
    {
        _dpUnitOfWork = dpUnitOfWork;
        _mapper = mapper;
        _accountStaffAssistantSetting = accountStaffAssistantConfig.Value;
    }

    public async Task<Result<Success<List<CreateMessageDto>>>> Handle(Query.GetMessagesSenderIdQuery request, CancellationToken cancellationToken)
    {
        var staff = await _dpUnitOfWork.AccountRepositories.GetByEmailAsync(_accountStaffAssistantSetting.Email);
        var result = await _dpUnitOfWork.MessageRepository.GetMessagesChatsAsync(request.SenderId, staff.Id);
        var createMessageDtos = _mapper.Map<List<CreateMessageDto>>(result);
        return Result.Success(new Success<List<CreateMessageDto>>("", "", createMessageDtos));
    }
}
