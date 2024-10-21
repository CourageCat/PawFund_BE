using AutoMapper;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Domain.Entities;
using static PawFund.Contract.Services.EventActivity.Respone;

namespace PawFund.Application.Mapper
{
    public class EventActivityProfile : Profile
    {
        public EventActivityProfile()
        {

            {
                CreateMap<EventActivity, GetEventActivityByIdDTO.ActivityDTO>();
                CreateMap<Event, GetEventActivityByIdDTO.EventDTO>();
                CreateMap<GetEventActivityByIdDTO.ActivityDTO, EventActivityResponse>()
                    .ConstructUsing(src => new EventActivityResponse(src, new GetEventActivityByIdDTO.EventDTO()));
            }
        }
    }
}
