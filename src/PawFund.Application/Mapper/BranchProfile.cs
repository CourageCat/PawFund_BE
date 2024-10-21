using AutoMapper;
using PawFund.Contract.DTOs.BranchDTOs;
using PawFund.Domain.Entities;

namespace PawFund.Application.Mapper;

public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<Branch, BranchEventDTO>();
    }
}
