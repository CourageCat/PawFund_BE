using AutoMapper;
using PawFund.Contract.DTOs.CatDTOs;
using PawFund.Domain.Entities;

namespace PawFund.Application.Mapper;

public class CatProfile : Profile
{
    public CatProfile()
    {
        CreateMap<Cat, CatDto>();
        CreateMap<ImageCat, ImageCatDto>();
    }
}
