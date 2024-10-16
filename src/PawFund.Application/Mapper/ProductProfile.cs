using AutoMapper;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Domain.Entities;
using static PawFund.Contract.Services.Products.Response;

namespace PawFund.Application.Mapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<PagedResult<Product>, PagedResult<ProductResponse>>().ReverseMap();
    }
}
