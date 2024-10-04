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
    public class CatRepository : ICatRepository
    {
        private readonly IConfiguration _configuration;

        public CatRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<int> AddAsync(Cat entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Cat entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<Cat>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Cat>? GetByIdAsync(Guid Id)
        {

            var sql = @"
        SELECT c.Id, c.Sex, c.Name, c.Age, c.Breed, c.Size, c.Color, c.Description
        FROM Cats c
        WHERE c.Id = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Cat>(
                    sql,
                    new { Id = Id }
                    );

                return result.FirstOrDefault();
            }
        }


        public Task<int> UpdateAsync(Cat entity)
        {
            throw new NotImplementedException();
        }
    }

