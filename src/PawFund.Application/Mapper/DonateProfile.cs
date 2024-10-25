using AutoMapper;
using PawFund.Contract.DTOs.DonateDTOs;
using PawFund.Domain.Entities;

namespace PawFund.Application.Mapper;

public class DonateProfile : Profile
{
    public DonateProfile()
    {
        CreateMap<Donation, DonateDto>();
    }
}
