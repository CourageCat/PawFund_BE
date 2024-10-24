using AutoMapper;
using PawFund.Contract.DTOs.Event;
using PawFund.Contract.DTOs.EventActivity;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.Event.Respone;

namespace PawFund.Application.Mapper
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {

            {
                CreateMap<Event, GetEventByIdDTO.EventDTO>();
                CreateMap<Branch, GetEventByIdDTO.BranchDTO>();
                //CreateMap<GetEventByIdDTO.EventDTO, EventResponse>()
                //    .ConstructUsing(src => new EventResponse(src, new GetEventByIdDTO.BranchDTO()));

                CreateMap<Event, EventDTO>();
            }
        }
    }
}
