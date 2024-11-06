
using AutoMapper;
using PawFund.Contract.DTOs.VolunteerApplicationDTOs.Respone;
using PawFund.Domain.Entities;

namespace PawFund.Application.Mapper
{
    public class VolunteerApplicationProfile : Profile
    {
        public VolunteerApplicationProfile()
        {
            CreateMap<VolunteerApplicationDetail, GetVolunteerApplicationById.VolunteerApplicationDTO>();
           
            CreateMap<VolunteerApplicationDetail, VolunteerApplicationsDTO>();

            CreateMap<Account, GetVolunteerApplicationById.AccountDTO>();
        }
    }
}
