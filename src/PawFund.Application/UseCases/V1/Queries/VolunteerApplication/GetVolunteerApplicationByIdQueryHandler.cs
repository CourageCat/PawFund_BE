using AutoMapper;
using Azure;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.VolunteerApplicationDetail;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone.GetVolunteerApplicationById;
using static PawFund.Contract.Services.VolunteerApplicationDetail.Respone;

namespace PawFund.Application.UseCases.V1.Queries.VolunteerApplication
{
    public sealed class GetVolunteerApplicationByIdQueryHandler : IQueryHandler<Query.GetVolunteerApplicationByIdQuery, Success<Respone.VolunteerApplicationResponse>>
    {
        private readonly IRepositoryBase<VolunteerApplicationDetail, Guid> _volunteerApplicationRepository;
        private readonly IDPUnitOfWork _dpUnitOfWork;
        private readonly IMapper _mapper;

        public GetVolunteerApplicationByIdQueryHandler(IRepositoryBase<VolunteerApplicationDetail, Guid> volunteerApplicationRepository, IDPUnitOfWork dpUnitOfWork, IMapper mapper)
        {
            _volunteerApplicationRepository = volunteerApplicationRepository;
            _dpUnitOfWork = dpUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Success<VolunteerApplicationResponse>>> Handle(Query.GetVolunteerApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var volunteerApplication = await _dpUnitOfWork.VolunteerApplicationDetailRepository.GetByIdAsync(request.Id);

            if (volunteerApplication != null)
            {
                //VolunteerApplicationDTO volunteerApplicationDTO = new VolunteerApplicationDTO()
                //{
                //    Id = request.Id,
                //    Description = volunteerApplication.Description,
                //    Status = volunteerApplication.Status.ToString(),
                //    ReasonReject = volunteerApplication.ReasonReject,
                //    EventId = volunteerApplication.EventId,
                //    EventActivityId = volunteerApplication.EventActivityId,
                //};
                AccountDTO accountDTO = new AccountDTO()
                {
                    Id = volunteerApplication.Account.Id,
                    Email = volunteerApplication.Account.Email,
                    FirstName = volunteerApplication.Account.FirstName,
                    LastName = volunteerApplication.Account.LastName,
                    PhoneNumber = volunteerApplication.Account.PhoneNumber,
                };

                VolunteerApplicationDTO volunteerApplicationDTO = _mapper.Map<VolunteerApplicationDTO>(volunteerApplication);

                var result = new Respone.VolunteerApplicationResponse(volunteerApplicationDTO, accountDTO);

                return Result.Success(new Success<Respone.VolunteerApplicationResponse>("", "", result));
            }
            else
            {
                throw new Domain.Exceptions.VolunteerApplicationException.VolunteerApplicationNotFoundException(request.Id);
            }
        }
    }
}
