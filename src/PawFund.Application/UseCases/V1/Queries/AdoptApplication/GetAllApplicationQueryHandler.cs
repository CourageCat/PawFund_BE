using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.DTOs.Adopt;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;

namespace PawFund.Application.UseCases.V1.Queries.Adopt;
public sealed class GetAllApplicationQueryHandler : IQueryHandler<Query.GetAllApplicationQuery, Response.GetAllApplicationResponse>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public GetAllApplicationQueryHandler(IDPUnitOfWork dpUnitOfWork)
    {
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result<Response.GetAllApplicationResponse>> Handle(Query.GetAllApplicationQuery request, CancellationToken cancellationToken)
    {
        //Find List Adopt Application
        var listAdoptApplicationFound = await _dpUnitOfWork.AdoptRepositories.GetAllApplicationsAsync();
        if (listAdoptApplicationFound.Count == 0)
        {
            throw new AdoptApplicationException.AdoptApplicationEmptyException();
        }

        //Convert Entity to DTO
        var listAdoptApplicationFoundDTO = new List<GetAllApplicationsDTO.AdoptApplicationDTO>();
        listAdoptApplicationFound.ForEach(adoptApplication =>
        {
            listAdoptApplicationFoundDTO.Add(new GetAllApplicationsDTO.AdoptApplicationDTO()
            {
                Id = adoptApplication.Id,
                MeetingDate = adoptApplication.MeetingDate,
                ReasonReject = adoptApplication.ReasonReject,
                Status = adoptApplication.Status.ToString(),
                IsFinalized = adoptApplication.IsFinalized,
                Description = adoptApplication.Description,
                Account = new GetAllApplicationsDTO.AccountDto()
                {
                    Id = adoptApplication.Account.Id,
                    FirstName = adoptApplication.Account.FirstName,
                    LastName = adoptApplication.Account.LastName,
                    Email = adoptApplication.Account.Email,
                    PhoneNumber = adoptApplication.Account.PhoneNumber,
                },
                Cat = new GetAllApplicationsDTO.CatDto()
                {
                    Id = adoptApplication.Cat.Id,
                    Sex = adoptApplication.Cat.Sex,
                    Name = adoptApplication.Cat.Name,
                    Age = adoptApplication.Cat.Age,
                    Breed = adoptApplication.Cat.Breed,
                    Size = adoptApplication.Cat.Size,
                    Color = adoptApplication.Cat.Color,
                    Description = adoptApplication.Cat.Description,
                }
            });
        });
        var result = new Response.GetAllApplicationResponse(listAdoptApplicationFoundDTO);

        //Return result
        return Result.Success(result);
    }
}

