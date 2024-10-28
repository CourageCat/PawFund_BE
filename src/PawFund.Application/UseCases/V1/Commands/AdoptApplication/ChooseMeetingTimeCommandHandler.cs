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
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Domain.Abstractions;

namespace PawFund.Application.UseCases.V1.Commands.AdoptApplication
{
    public sealed class ChooseMeetingTimeCommandHandler : ICommandHandler<Command.ChooseMeetingTimeCommand>
    {
        private readonly IResponseCacheService _responseCacheService;
        private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptRepository;
        private readonly IEFUnitOfWork _efUnitOfWork;

        public ChooseMeetingTimeCommandHandler(IResponseCacheService responseCacheService, IRepositoryBase<AdoptPetApplication, Guid> adoptRepository, IEFUnitOfWork efUnitOfWork)
        {
            _responseCacheService = responseCacheService;
            _adoptRepository = adoptRepository;
            _efUnitOfWork = efUnitOfWork;
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
            bool isMeetingTimeExisted = false;
            //Check if meeting time still exist
            foreach (var meetingTime in listMeetingTime)
            {
                if (meetingTime.MeetingTime.Equals(request.MeetingTime))
                {
                    isMeetingTimeExisted = true;
                    break;
                }
            }
            if (!isMeetingTimeExisted)
            {
                throw new AdoptApplicationException.NotFoundMeetingTimeException();
            }
            //Update meeting time and staff free
            var listUpdatedMeetingTime = listMeetingTime.Select(x =>
            {
                //Check Meeting Time Exist
                if (x.MeetingTime.Equals(request.MeetingTime))
                {
                    //Check Has Any Staff Free in this Meeting Time
                    if (x.NumberOfStaffsFree > 0)
                    {
                        return new GetMeetingTimeByAdopterResponseDTO.MeetingTimeDTO()
                        {
                            MeetingTime = x.MeetingTime,
                            NumberOfStaffsFree = x.NumberOfStaffsFree - 1
                        };
                    }
                    else
                    {
                        throw new AdoptApplicationException.NoStaffFreesForTheMeetingTimeException();
                    }
                }
                return new GetMeetingTimeByAdopterResponseDTO.MeetingTimeDTO()
                {
                    MeetingTime = x.MeetingTime,
                    NumberOfStaffsFree = x.NumberOfStaffsFree
                };
            }).ToList();
            //Find farthest time and set expired time for key in redis
            //ExpiredTime = farthestTime - DateTime.Now + 0.5Day
            var fartheseTime = listUpdatedMeetingTime.Max(x => x.MeetingTime);
            var expiredTime = (fartheseTime - DateTime.Now).Add(TimeSpan.FromDays(0.5));

            //Add time to Redis (Key: Branch Name, Value: List of time)
            await _responseCacheService.SetListAsync(branchName, listUpdatedMeetingTime, expiredTime);
            applicationFound.MeetingDate = request.MeetingTime;
            _adoptRepository.Update(applicationFound);
            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new Success(MessagesList.AdoptChooseMeetingTimeSuccess.GetMessage().Code, MessagesList.AdoptChooseMeetingTimeSuccess.GetMessage().Message));
        }
    }
}
