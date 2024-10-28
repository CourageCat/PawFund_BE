using AutoMapper;
using PawFund.Contract.DTOs.ChatHistoryDTOs;
using PawFund.Domain.Entities;

namespace PawFund.Application.Mapper;

public class ChatHistoryProfile : Profile
{
    public ChatHistoryProfile()
    {
        CreateMap<ChatHistory, ChatHistoryDto>();
    }
}
