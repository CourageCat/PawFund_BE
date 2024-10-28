using AutoMapper;
using PawFund.Contract.DTOs.MessageDTOs;

namespace PawFund.Application.Mapper;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Domain.Entities.Message, CreateMessageDto>();
    }
}
