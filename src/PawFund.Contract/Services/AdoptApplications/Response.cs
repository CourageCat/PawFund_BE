using PawFund.Contract.DTOs.Adopt.Response;
using PawFund.Contract.Enumarations.AdoptPetApplication;
namespace PawFund.Contract.Services.AdoptApplications;

public static class Response
{
    public record GetApplicationByIdResponse
        (Guid Id,
        DateTime? MeetingDate,
        string? ReasonReject,
        string Status,
        string Description,
        bool IsFinalized,
        GetApplicationByIdResponseDTO.AccountDto Account,
        GetApplicationByIdResponseDTO.CatDto Cat);

    public record GetAllApplicationResponse(List<GetAllApplicationsResponseDTO.AdoptApplicationDTO> List);


}