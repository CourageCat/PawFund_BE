using AutoMapper;
using PawFund.Contract.DTOs.EventDTOs.Respone;
using PawFund.Domain.Entities;

namespace PawFund.Application.Mapper;

public class EventProfile : Profile
{
    public EventProfile()
    {

        {
            CreateMap<Event, EventForUserDTO.EventDTO>();
            CreateMap<Branch, EventForUserDTO.BranchDTO>();
            CreateMap<Event, EventForAdminStaffDTO>();
        }
    }
}
