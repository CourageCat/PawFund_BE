using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Adopt.Response;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.AdoptApplications;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using PawFund.Domain.Abstractions.Repositories;
using PawFund.Domain.Entities;
using PawFund.Domain.Exceptions;
using static PawFund.Contract.Services.AdoptApplications.Response;

namespace PawFund.Application.UseCases.V1.Queries.Adopt;
public sealed class GetAllApplicationQueryByAdopterHandler : IQueryHandler<Query.GetAllApplicationByAdopterQuery, Success<PagedResult<ApplicationResponse>>>
{
    private readonly IDPUnitOfWork _dpUnitOfWork;

    public GetAllApplicationQueryByAdopterHandler(IDPUnitOfWork dpUnitOfWork)
    {
        _dpUnitOfWork = dpUnitOfWork;
    }

    public async Task<Result<Success<PagedResult<ApplicationResponse>>>> Handle(Query.GetAllApplicationByAdopterQuery request, CancellationToken cancellationToken)
    {
        //Find List Adopt Application
        var listAdoptApplicationFoundPaging = await _dpUnitOfWork.AdoptRepositories.GetAllApplicationsByAdopterAsync(request.AccountId, request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);
        var listAdoptApplicationFoundDTO = new List<ApplicationResponse>();

        //Count TotalPages
        decimal totalPages = Math.Ceiling((decimal)(listAdoptApplicationFoundPaging.Items.Count / request.PageSize));

        //Mapping Entities to DTO
        listAdoptApplicationFoundPaging.Items.ForEach(adoptApplication =>
        {

            listAdoptApplicationFoundDTO.Add(new ApplicationResponse(new GetAllApplicationsResponseDTO.AdoptApplicationDTO()
            {
                Id = adoptApplication.Id,
                MeetingDate = adoptApplication.MeetingDate,
                ReasonReject = adoptApplication.ReasonReject,
                Status = adoptApplication.Status,
                IsFinalized = adoptApplication.IsFinalized,
                Description = adoptApplication.Description,
                CreatedDate = (DateTime)adoptApplication.CreatedDate,
                Account = new GetAllApplicationsResponseDTO.AccountDto()
                {
                    Id = adoptApplication.Account.Id,
                    FirstName = adoptApplication.Account.FirstName,
                    LastName = adoptApplication.Account.LastName,
                    Email = adoptApplication.Account.Email,
                    PhoneNumber = adoptApplication.Account.PhoneNumber,
                },
                Cat = new GetAllApplicationsResponseDTO.CatDto()
                {
                    Id = adoptApplication.Cat.Id,
                    Sex = adoptApplication.Cat.Sex.ToString(),
                    Name = adoptApplication.Cat.Name,
                    Age = adoptApplication.Cat.Age,
                    Breed = adoptApplication.Cat.Breed,
                    Weight = adoptApplication.Cat.Weight,
                    Color = adoptApplication.Cat.Color,
                    Description = adoptApplication.Cat.Description,
                    Sterilization = adoptApplication.Cat.Sterilization,
                    ImageUrl = adoptApplication.Cat.ImageCats.ToList()[0].ImageUrl,
                    PublicImageId = adoptApplication.Cat.ImageCats.ToList()[0].PublicImageId
                }
            }));
        });
        var result = new PagedResult<ApplicationResponse>(listAdoptApplicationFoundDTO, listAdoptApplicationFoundPaging.PageSize, listAdoptApplicationFoundPaging.PageSize, listAdoptApplicationFoundPaging.TotalCount,
            listAdoptApplicationFoundPaging.TotalPages);

        //if (listAdoptApplicationFound.Count == 0)
        //{
        //    throw new AdoptApplicationException.AdoptApplicationEmptyException();
        //}

        ////Convert Entity to DTO
        //var listAdoptApplicationFoundDTO = new List<GetAllApplicationsResponseDTO.AdoptApplicationDTO>();
        //listAdoptApplicationFound.ForEach(adoptApplication =>
        //{
        //        listAdoptApplicationFoundDTO.Add(new GetAllApplicationsResponseDTO.AdoptApplicationDTO()
        //        {
        //            Id = adoptApplication.Id,
        //            MeetingDate = adoptApplication.MeetingDate,
        //            ReasonReject = adoptApplication.ReasonReject,
        //            Status = adoptApplication.Status.ToString(),
        //            IsFinalized = adoptApplication.IsFinalized,
        //            Description = adoptApplication.Description,
        //            Account = new GetAllApplicationsResponseDTO.AccountDto()
        //            {
        //                Id = adoptApplication.Account.Id,
        //                FirstName = adoptApplication.Account.FirstName,
        //                LastName = adoptApplication.Account.LastName,
        //                Email = adoptApplication.Account.Email,
        //                PhoneNumber = adoptApplication.Account.PhoneNumber,
        //            },
        //            Cat = new GetAllApplicationsResponseDTO.CatDto()
        //            {
        //                Id = adoptApplication.Cat.Id,
        //                Sex = adoptApplication.Cat.Sex,
        //                Name = adoptApplication.Cat.Name,
        //                Age = adoptApplication.Cat.Age,
        //                Breed = adoptApplication.Cat.Breed,
        //                Size = adoptApplication.Cat.Size,
        //                Color = adoptApplication.Cat.Color,
        //                Description = adoptApplication.Cat.Description,
        //            }
        //        });
        //});
        //var result = new Response.GetAllApplicationResponse(listAdoptApplicationFoundDTO);
        //Check if list empty then return empty message
        if (result.Items.Count == 0)
        {
            return Result.Success(new Success<PagedResult<ApplicationResponse>>(MessagesList.AdoptApplicationEmptyException.GetMessage().Code, MessagesList.AdoptApplicationEmptyException.GetMessage().Message, result));
        }

        //Return result
        return Result.Success(new Success<PagedResult<ApplicationResponse>>(MessagesList.AdoptGetAdoptApplicationsSuccess.GetMessage().Code, MessagesList.AdoptGetAdoptApplicationsSuccess.GetMessage().Message, result));
    }
}

