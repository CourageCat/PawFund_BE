using AutoMapper;
using PawFund.Contract.DTOs.BranchDTOs;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Entities;
using static PawFund.Contract.Services.Branchs.Response;

namespace PawFund.Application.Mapper;

public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<Branch, BranchEventDTO>();
        CreateMap<Branch, BranchResponse>();
        CreateMap<PagedResult<Branch>, PagedResult<BranchResponse>>();
    }
}
