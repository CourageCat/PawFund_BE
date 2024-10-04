using PawFund.Contract.DTOs.Adopt;
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