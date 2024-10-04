using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Infrastructure.Dapper.Repositories;

    public class AdoptRepository : IAdoptRepository
    {
        private readonly IConfiguration _configuration;

        public AdoptRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<int> AddAsync(AdoptPetApplication entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(AdoptPetApplication entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<AdoptPetApplication>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AdoptPetApplication?> GetByIdAsync(Guid Id)
        {
            var sql = @"
        SELECT 
            a.Id, a.MeetingDate, a.Status, a.IsFinalized, a.Description, a.IsDeleted as IsAdoptDeleted,
            acc.Id, acc.FirstName, acc.LastName, acc.Email, acc.PhoneNumber, acc.IsDeleted as IsAccountDeleted,
            c.Id, c.Sex, c.Name, c.Age, c.Breed, c.Size, c.Color, c.Description
        FROM AdoptPetApplications a
        JOIN Accounts acc ON acc.Id = a.AccountId
        JOIN Cats c ON c.Id = a.CatId
        WHERE a.Id = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<AdoptPetApplication, Account, Cat, AdoptPetApplication>(
                    sql,
                    (adoptPetApplication, account, cat) =>
                    {
                        adoptPetApplication.Account = account;
                        adoptPetApplication.Cat = cat;
                        return adoptPetApplication;
                    },
                    new { Id = Id },
                    splitOn: "IsAdoptDeleted,IsAccountDeleted"
                );

                return result.FirstOrDefault();
            }
        }


        public Task<int> UpdateAsync(AdoptPetApplication entity)
        {
            throw new NotImplementedException();
        }
    }

