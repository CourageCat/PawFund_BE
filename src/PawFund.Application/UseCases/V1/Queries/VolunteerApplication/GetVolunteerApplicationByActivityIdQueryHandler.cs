using AutoMapper;
using Azure.Core;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using static PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone.GetVolunteerApplicationById;
using static PawFund.Domain.Exceptions.VolunteerApplicationException;

namespace PawFund.Application.UseCases.V1.Queries.VolunteerApplication
{
    public sealed class GetVolunteerApplicationByActivityIdQueryHandler : IQueryHandler<Query.GetVolunteerApplicationByActivityQuery, Success<PagedResult<VolunteerApplicationsDTO>>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;
        private readonly IMapper _mapper;

        public GetVolunteerApplicationByActivityIdQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
        {
            _dpUnitOfWork = dpUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Success<PagedResult<VolunteerApplicationsDTO>>>> Handle(Query.GetVolunteerApplicationByActivityQuery request, CancellationToken cancellationToken)
        {
            var result = await _dpUnitOfWork.VolunteerApplicationDetailRepository
    .GetAllVolunteerAppicationByActivityIdAsync(request.Id, request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);

            if (result != null && result.Items != null && result.Items.Count > 0)
            {
                var volunteerApplicationDto = result.Items
                    .Where(item => item != null) // Ensure item is not null
                    .Select(item => new VolunteerApplicationsDTO
                    {
                        Id = item.Id,
                        Status = item.Status,
                        Description = item.Description,
                        ReasonReject = item.ReasonReject,
                        EventId = item.EventId,
                        EventActivityId = item.EventActivityId,
                        Account = item.Account != null ? new AccountDTO // Ensure item.Account is not null
                        {
                            Id = item.Account.Id,
                            FirstName = item.Account.FirstName,
                            LastName = item.Account.LastName,
                            Email = item.Account.Email,
                            PhoneNumber = item.Account.PhoneNumber
                        } : null // Set to null if item.Account is null
                    })
                    .ToList();

                return Result.Success(new Success<PagedResult<VolunteerApplicationsDTO>>(
                    MessagesList.GetVolunteerApplicationSuccess.GetMessage().Code,
                    MessagesList.GetVolunteerApplicationSuccess.GetMessage().Message,
                    new PagedResult<VolunteerApplicationsDTO>(volunteerApplicationDto, result.PageIndex, result.PageSize, result.TotalCount, result.TotalPages)
                ));
            }
            else
            {
                throw new VolunteerApplicationNotFoundByActivityIdException(request.Id);
            }

        }
    }
}
