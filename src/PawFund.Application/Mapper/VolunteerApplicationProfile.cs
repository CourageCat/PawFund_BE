
using AutoMapper;
using PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone;
using PawFund.Domain.Entities;

namespace PawFund.Application.Mapper
{
    public class VolunteerApplicationProfile : Profile
    {
        public VolunteerApplicationProfile()
        {
            CreateMap<VolunteerApplicationProfile, GetVolunteerApplicationById.VolunteerApplicationDTO>();
            CreateMap<Account, GetVolunteerApplicationById.AccountDTO>();
            CreateMap<VolunteerApplicationProfile, VolunteerApplicationsDTO>();
        }
    }
}
