using PawFund.Contract.Abstractions.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Shared;
using PawFund.Contract.Abstractions.Services;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;
using PawFund.Contract.DTOs.Adopt.Response;

namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication
{
    public sealed class ChooseMeetingTimeCommandHandler : ICommandHandler<Command.ChooseMeetingTimeCommand>
    {
        private readonly IResponseCacheService _responseCacheService;
        private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptRepository;

        public ChooseMeetingTimeCommandHandler(IResponseCacheService responseCacheService, IRepositoryBase<AdoptPetApplication, Guid> adoptRepository)
        {
            _responseCacheService = responseCacheService;
            _adoptRepository = adoptRepository;
        }

        public async Task<Result> Handle(Command.ChooseMeetingTimeCommand request, CancellationToken cancellationToken)
        {
            var applicationFound = await _adoptRepository.FindByIdAsync(request.AdoptId);
            if (applicationFound == null)
            {
                throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.AdoptId);
            }
            var branchName = applicationFound.Cat.Branch.Name;
            var listMeetingTime = await _responseCacheService.GetListAsync<GetMeetingTimeByAdopterResponseDTO.MeetingTimeDTO>(branchName);
            var listUpdatedMeetingTime = listMeetingTime.Select(x =>
            {
                //Check Meeting Time Exist
                if (x.MeetingTime.Equals(request.MeetingTime) )
                {
                    //Check Has Any Staff Free in this Meeting Time
                    if(x.NumberOfStaffsFree > 0)
                    {
                        return new GetMeetingTimeByAdopterResponseDTO.MeetingTimeDTO()
                        {
                            MeetingTime = x.MeetingTime,
                            NumberOfStaffsFree = x.NumberOfStaffsFree - 1
                        };
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                throw new Exception();
            });
            throw new Exception();
        }
    }
}
