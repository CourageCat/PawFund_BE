using MediatR;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;
using PawFund.Contract.DTOs.Adopt.Response;


namespace PawFund.Application.UseCases.V1.Commands.Adopt;
public sealed class GetApplicationByIdQueryHandler : IQueryHandler<Query.GetApplicationByIdQuery, Response.GetApplicationByIdResponse>
{
    private readonly IRepositoryBase<AdoptPetApplication, Guid> _adoptRepository;
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public GetApplicationByIdQueryHandler(IRepositoryBase<AdoptPetApplication, Guid> adoptRepository, IDPUnitOfWork dPUnitOfWork)
    {
        _adoptRepository = adoptRepository;
        _dpUnitOfWork = dPUnitOfWork;
    }

    public async Task<Result<Response.GetApplicationByIdResponse>> Handle(Query.GetApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        //Check Application found
        var applicationById = await _dpUnitOfWork.AdoptRepositories.GetByIdAsync(request.Id);
        if (applicationById == null)
        {
            throw new AdoptApplicationException.AdoptApplicationNotFoundException(request.Id);
        }

        //Convert Entity to DTO
        var result = new Response.GetApplicationByIdResponse(applicationById.Id, applicationById.MeetingDate, applicationById.ReasonReject, applicationById.Status.ToString(), applicationById.Description, applicationById.IsFinalized,
            new GetApplicationByIdResponseDTO.AccountDto
            {
                Id = applicationById.Account.Id,
                FirstName = applicationById.Account.FirstName,
                LastName = applicationById.Account.LastName,
                Email = applicationById.Account.Email,
                PhoneNumber = applicationById.Account.PhoneNumber,
            },
            new GetApplicationByIdResponseDTO.CatDto
            {
                Id = applicationById.Cat.Id,
                Sex = applicationById.Cat.Sex,
                Name = applicationById.Cat.Name,
                Age = applicationById.Cat.Age,
                Breed = applicationById.Cat.Breed,
                Size = applicationById.Cat.Size,
                Color = applicationById.Cat.Color,
                Description = applicationById.Cat.Description,
            });

        //Return result
        return Result.Success(result);
    }


}

