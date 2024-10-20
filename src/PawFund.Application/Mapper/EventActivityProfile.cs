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

                //     CreateMap<EventActivity, GetEventActivityByIdDTO.ActivityDTO>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Event.Id))
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Event.Name));

                //     CreateMap<Event, GetEventActivityByIdDTO.EventDTO>();

                //     CreateMap<EventActivityResponse, EventActivityResponse>()
                //         .ForMember(dest => dest.ActivityDTO, opt => opt.MapFrom(src => src.ActivityDTO))
                //         .ForMember(dest => dest.EventDTO, opt => opt.MapFrom(src => src.EventDTO));
            }
        }
    }
}
