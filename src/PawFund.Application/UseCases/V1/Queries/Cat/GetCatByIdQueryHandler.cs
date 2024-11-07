using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.CatDTOs;
using PawFund.Contract.Services.Cats;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Queries.Cat;
public sealed class GetCatByIdQueryHandler : IQueryHandler<Query.GetCatByIdQuery, Success<Response.CatResponse>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public GetCatByIdQueryHandler(IDPUnitOfWork dpUnitOfWork)
    {
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result<Success<Response.CatResponse>>> Handle(Query.GetCatByIdQuery request, CancellationToken cancellationToken)
    {
        var catFound = await _dpUnitOfWork.CatRepositories.GetCatByIdAsync(request.Id);
        if (catFound == null || catFound.IsDeleted == true)
        {
            throw new CatException.CatNotFoundException();
        }
        var catImages = catFound.ImageCats.Select(item => new ImageCatDto
        {
            Id = item.Id,
            ImageUrl = item.ImageUrl
        }).ToList();
        if (request.AccountId != Guid.Empty)
        {
            bool hasAccountRegisteredWithCat = await _dpUnitOfWork.AdoptRepositories.HasAccountRegisterdWithPetAsync(request.AccountId, request.Id);
            var result = new Response.CatResponse(catFound.Id, catFound.Sex.ToString(), catFound.Name, catFound.Age, catFound.Breed, catFound.Weight, catFound.Color, catFound.Description, catFound.Sterilization, catImages, hasAccountRegisteredWithCat);
            return Result.Success(new Success<Response.CatResponse>("", "", result));
        }
        else
        {
            var result = new Response.CatResponse(catFound.Id, catFound.Sex.ToString(), catFound.Name, catFound.Age, catFound.Breed, catFound.Weight, catFound.Color, catFound.Description, catFound.Sterilization, catImages, false);
            return Result.Success(new Success<Response.CatResponse>("", "", result));
        }
    }
}

