using PawFund.Contract.DTOs.Adopt;
namespace PawFund.Contract.Services.AdoptApplications;

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

    public record GetAllApplicationResponse();

    
}