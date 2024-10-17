using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.DTOs.Adopt.Request;
namespace PawFund.Contract.Services.AdoptApplications;

public static class Command
{
    public record CreateAdoptApplicationCommand
        (string Description,
        Guid AccountId,
        Guid CatId)
        : ICommand;

    public record UpdateAdoptApplicationCommand
        (Guid AdoptId,
        string Description,
        Guid? CatId)
        : ICommand;

    public record DeleteAdoptApplicationByAdopterCommand
        (Guid AdoptId)
        : ICommand;

    public record UpdateMeetingTimeCommand(Guid AccountId, List<UpdateMeetingTimeRequestDTO.MeetingTimeDTO> ListTime) : ICommand;

    public record ApplyAdoptApplicationCommand(Guid AdoptId) : ICommand;
    public record RejectAdoptApplicationCommand(Guid AdoptId, string ReasonReject) : ICommand;

    public record UpdateDataFromGoogleSheetCommand(string Branch, string Time, int NumberOfStaffsFree) : ICommand;
}
