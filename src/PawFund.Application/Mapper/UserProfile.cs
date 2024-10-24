using AutoMapper;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Entities;
using static PawFund.Contract.Services.Accounts.Response;

namespace PawFund.Application.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Account, UsersResponse>().ReverseMap();
        CreateMap<PagedResult<Account>, PagedResult<UsersResponse>>().ReverseMap();
    }
}
