using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Account;
using PawFund.Contract.DTOs.DonateDTOs;
using PawFund.Contract.Services.Admin;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions;
using PawFund.Domain.Abstractions.Dappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.Accounts.Response;
using static PawFund.Contract.Services.Admin.Response;

namespace PawFund.Application.UseCases.V1.Queries.Admin
{
    public sealed class GetUsersByYearQueryHandler : IQueryHandler<Query.GetUsersByYearQuery, Success<UsersByYearResponse>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;
        private readonly IMapper _mapper;

        public GetUsersByYearQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
        {
            _dpUnitOfWork = dpUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Success<UsersByYearResponse>>> Handle(Query.GetUsersByYearQuery request, CancellationToken cancellationToken)
        {
            var listUsers = await _dpUnitOfWork.AccountRepositories.FindAllUsersByYear(request.Year);
            var totalCustomers = listUsers.Values.Sum(list => list.Count());
            var customersGroupByMonth = new List<GetUserByYearDTO.MonthDTO>();
            foreach (var item in listUsers.Keys)
            {
                var customers = _mapper.Map<List<GetUserByYearDTO.CustomerDTO>>(listUsers[item]);
                //result[item] = listUsers[item].ToList().Select(x => new UsersResponse(x.Id, x.FirstName, x.LoginType.ToString(), x.Email, x.PhoneNumber, x.IsDeleted.Value, x.LoginType, x.Gender, x.CropAvatarUrl, x.CropAvatarId, x.FullAvatarUrl, x.FullAvatarId, x.CreatedDate));
                var customersCount = customers.Count();
                var monthDTO = new GetUserByYearDTO.MonthDTO()
                {
                    MonthOfYear = item,
                    CustomersCount = customersCount,
                    Customers = customers,
                };
                customersGroupByMonth.Add(monthDTO);
            }
            var result = new UsersByYearResponse(totalCustomers, customersGroupByMonth);
            return Result.Success(new Success<UsersByYearResponse>("", "", result));
        }
    }
}
