using PawFund.Contract.DTOs.CatDTOs;
using PawFund.Contract.DTOs.MediaDTOs;

namespace PawFund.Contract.Services.Cats;

public static class Response
{
    public record CatResponse(Guid Id, string Sex, string Name, string Age, string Breed, decimal Weight, string Color, string Description, bool Sterilization, List<ImageCatDto> Images, bool HasUserAdoptCat);
}
