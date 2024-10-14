using AutoMapper;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Entities;
using static PawFund.Contract.Services.Products.Response;

namespace PawFund.Application.Mapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<PagedResult<Product>, PagedResult<ProductResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<PagedResult<ProductResponse>, PagedResult<Product>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
