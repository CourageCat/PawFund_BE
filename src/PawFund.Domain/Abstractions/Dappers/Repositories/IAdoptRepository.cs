using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.DTOs.Adopt;
using PawFund.Contract.Services.Products;
using PawFund.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawFund.Contract.Services.AdoptApplications.Filter;

namespace PawFund.Domain.Abstractions.Dappers.Repositories;

public interface IAdoptRepository : IGenericRepository<AdoptPetApplication>
{
    Task<bool> HasAccountRegisterdWithPetAsync(Guid accountId, Guid catId);
    //Task<PagedResult<AdoptPetApplication>> GetAllApplicationsAsync(int pageIndex, int pageSize, bool isAscCreatedDate, string[] selectedColumns);
    Task<PagedResult<AdoptPetApplication>> GetAllApplicationsByAdopterAsync(Guid accountId, int pageIndex, int pageSize, AdoptApplicationFilter filterParams, string[] selectedColumns);
    Task<PagedResult<AdoptPetApplication>> GetAllApplicationByStaffAsync(Guid accountId, int pageIndex, int pageSize, AdoptApplicationFilter filterParams, string[] selectedColumns);
    Task<IEnumerable<AdoptPetApplication>> GetAllApplicationsByCatId(Guid AdoptId, Guid catId);
    Task<int> CountAllAdoptApplications();
}

