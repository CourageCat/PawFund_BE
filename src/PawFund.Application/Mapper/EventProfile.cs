using AutoMapper;
using PawFund.Contract.DTOs.Event;
using PawFund.Domain.Entities;

namespace PawFund.Application.Mapper;

public class EventProfile : Profile
{
    public EventProfile()
    {

        {
            CreateMap<Event, GetEventByIdDTO.EventDTO>();
            CreateMap<Branch, GetEventByIdDTO.BranchDTO>();
            CreateMap<Event, EventDTO>();
        }
    }
}
