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
    public sealed class GetUsersByYearAndMonthQueryHandler : IQueryHandler<Query.GetUsersByYearAndMonthQuery, Success<UsersByYearAndMonthResponse>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;
        private readonly IMapper _mapper;
        private readonly int firstDayOfWeek1 = 1;
        private readonly int firstDayOfWeek2 = 8;
        private readonly int firstDayOfWeek3 = 15;
        private readonly int firstDayOfWeek4 = 21;
        private readonly int firstDayOfWeek5 = 29;
        private readonly int totalDaysIn4Weeks = 28;


        public GetUsersByYearAndMonthQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
        {
            _dpUnitOfWork = dpUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Success<UsersByYearAndMonthResponse>>> Handle(Query.GetUsersByYearAndMonthQuery request, CancellationToken cancellationToken)
        {
            var listUsers = await _dpUnitOfWork.AccountRepositories.FindAllUsersByYearAndMonth(request.Year, request.Month);
            var totalCustomers = listUsers.Values.Sum(list => list.Count());
            var customersGroupByWeek = new List<GetUserByYearAndMonthDTO.WeekDTO>();
            var week1DTO = new GetUserByYearAndMonthDTO.WeekDTO();
            week1DTO.Week = 1;
            var week2DTO = new GetUserByYearAndMonthDTO.WeekDTO();
            week2DTO.Week = 2;
            var week3DTO = new GetUserByYearAndMonthDTO.WeekDTO();
            week3DTO.Week = 3;
            var week4DTO = new GetUserByYearAndMonthDTO.WeekDTO();
            week4DTO.Week = 4;
            var week5DTO = new GetUserByYearAndMonthDTO.WeekDTO();
            week5DTO.Week = 5;
            foreach (var item in listUsers.Keys)
            {
                var customers = _mapper.Map<List<GetUserByYearAndMonthDTO.CustomerDTO>>(listUsers[item]);
                var customersCountInDay = customers.Count();
                if (Math.Ceiling(((item / (double)totalDaysIn4Weeks)) * 4) == 1)
                {
                    var dayOfWeek = item - firstDayOfWeek1 + 1;
                    var dayDTO = new GetUserByYearAndMonthDTO.DayDTO()
                    {
                        Customers = customers,
                        CustomersCount = customersCountInDay,
                        DayOfWeek = dayOfWeek
                    };
                    week1DTO.Days.Add(dayDTO);
                    continue;
                }

                if (Math.Ceiling(((item / (double)totalDaysIn4Weeks)) * 4) == 2)
                {
                    var dayOfWeek = item - firstDayOfWeek2 + 1;
                    var dayDTO = new GetUserByYearAndMonthDTO.DayDTO()
                    {
                        Customers = customers,
                        CustomersCount = customersCountInDay,
                        DayOfWeek = dayOfWeek
                    };
                    week2DTO.Days.Add(dayDTO);
                    continue;
                }

                if (Math.Ceiling(((item / (double)totalDaysIn4Weeks)) * 4) == 3)
                {
                    var dayOfWeek = item - firstDayOfWeek3 + 1;
                    var dayDTO = new GetUserByYearAndMonthDTO.DayDTO()
                    {
                        Customers = customers,
                        CustomersCount = customersCountInDay,
                        DayOfWeek = dayOfWeek
                    };
                    week3DTO.Days.Add(dayDTO);
                    continue;
                }

                if (Math.Ceiling(((item / (double)totalDaysIn4Weeks)) * 4) == 4)
                {
                    var dayOfWeek = item - firstDayOfWeek4 + 1;
                    var dayDTO = new GetUserByYearAndMonthDTO.DayDTO()
                    {
                        Customers = customers,
                        CustomersCount = customersCountInDay,
                        DayOfWeek = dayOfWeek
                    };
                    week4DTO.Days.Add(dayDTO);
                    continue;
                }

                if (Math.Ceiling(((item / (double)totalDaysIn4Weeks)) * 4) == 5)
                {
                    var dayOfWeek = item - firstDayOfWeek5 + 1;
                    var dayDTO = new GetUserByYearAndMonthDTO.DayDTO()
                    {
                        Customers = customers,
                        CustomersCount = customersCountInDay,
                        DayOfWeek = dayOfWeek
                    };
                    week5DTO.Days.Add(dayDTO);
                    continue;
                }
            }
            if(week1DTO.Days.Count != 0)
            {
                week1DTO.CustomersCount = week1DTO.Days.Sum(day => day.CustomersCount);
                customersGroupByWeek.Add(week1DTO);
            }
            if (week2DTO.Days.Count != 0)
            {
                week2DTO.CustomersCount = week2DTO.Days.Sum(day => day.CustomersCount);
                customersGroupByWeek.Add(week2DTO);
            }
            if (week3DTO.Days.Count != 0)
            {
                week3DTO.CustomersCount = week3DTO.Days.Sum(day => day.CustomersCount);
                customersGroupByWeek.Add(week3DTO);
            }
            if (week4DTO.Days.Count != 0)
            {
                week4DTO.CustomersCount = week4DTO.Days.Sum(day => day.CustomersCount);
                customersGroupByWeek.Add(week4DTO);
            }
            if (week5DTO.Days.Count != 0)
            {
                week5DTO.CustomersCount = week5DTO.Days.Sum(day => day.CustomersCount);
                customersGroupByWeek.Add(week5DTO);
            }
            var result = new UsersByYearAndMonthResponse(totalCustomers, customersGroupByWeek);
            return Result.Success(new Success<UsersByYearAndMonthResponse>("", "", result));
        }
    }
}
