using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.DonateDTOs;
using PawFund.Contract.Enumarations.Cat;
using PawFund.Contract.Enumarations.MessagesList;
using PawFund.Contract.Services.Admin;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.Admin.Response;

namespace PawFund.Application.UseCases.V1.Queries.Admin
{
    public sealed class GetDashboardQueryHandler : IQueryHandler<Query.GetDashboardQuery, Success<DashboardResponse>>
    {
        private readonly IDPUnitOfWork _dpUnitOfWork;
        private readonly IMapper _mapper;

        public GetDashboardQueryHandler(IDPUnitOfWork dpUnitOfWork, IMapper mapper)
        {
            _dpUnitOfWork = dpUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Success<DashboardResponse>>> Handle(Query.GetDashboardQuery request, CancellationToken cancellationToken)
        {
            //Count total for dashboard
            int totalCats = await _dpUnitOfWork.CatRepositories.CountAllCats();
            int totalAdoptApplications = await _dpUnitOfWork.AdoptRepositories.CountAllAdoptApplications();
            int totalEvents = await _dpUnitOfWork.EventRepository.CountAllEvents();
            double totalAmountDonations = await _dpUnitOfWork.DonationRepository.GetTotalAmountOfDonation();
            int totalVolunteerApplications = await _dpUnitOfWork.VolunteerApplicationDetailRepository.CountAllVolunteerApplications();
            int totalAccounts = await _dpUnitOfWork.AccountRepositories.CountAllUsers();

            //Calculate amount donation in year
            List<double> listDonationInYear = await _dpUnitOfWork.DonationRepository.CountAmountInYear(request.Year);
            var listMonthNames = new List<string>
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };
            var userDonated = await _dpUnitOfWork.AccountRepositories.GetAccountDonated();
            List<AccountDonateDashboardDTO> listTopFiveUserDonated = new List<AccountDonateDashboardDTO>();
            for (int i = 0; i < userDonated.Count; i++)
            {
                listTopFiveUserDonated.Add(new AccountDonateDashboardDTO()
                {
                    ImageUrl = userDonated[i].CropAvatarUrl,
                    Email = userDonated[i].Email,
                    Amount = userDonated[i].Donations.Sum(x => x.Amount),
                    Percentage = Math.Ceiling(userDonated[i].Donations.Sum(x => x.Amount) / (double)totalAmountDonations) * 100,
                });
            }

            //Return result
            var result = new DashboardResponse(totalCats, totalAdoptApplications, totalEvents, totalAmountDonations, totalVolunteerApplications, totalAccounts, listMonthNames, listDonationInYear, listTopFiveUserDonated);
            return Result.Success(new Success<DashboardResponse>(MessagesList.GetDashboardForTotalSuccess.GetMessage().Code, MessagesList.GetDashboardForTotalSuccess.GetMessage().Message, result));

        }
    }
}
