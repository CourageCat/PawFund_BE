using PawFund.Domain.DTOs.Adopt;
using PawFund.Domain.Entities;

namespace PawFund.Contract.Services.Adopt;

public static class Response
{
    public record GetApplicationByIdResponse
        (Guid Id,
        DateTime? MeetingDate,
        int Status,
        string Description,
        bool IsFinalized,
        GetApplicationByIdDTO.AccountDto Account,
        GetApplicationByIdDTO.CatDto Cat);

    
}