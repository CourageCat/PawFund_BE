using AutoMapper;
using PawFund.Contract.Abstractions.Message;
using PawFund.Contract.Abstractions.Shared;
using PawFund.Contract.Services.Products;
using PawFund.Contract.Shared;
using PawFund.Domain.Abstractions.Dappers.Repositories;
using static PawFund.Contract.Services.Products.Response;

namespace PawFund.Application.UseCases.V1.Queries.Product;

public class GetProductsPaginQueryHandler : IQueryHandler<Query.GetProductsPaginQueryHandler, PagedResult<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetProductsPaginQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<PagedResult<ProductResponse>>> Handle(Query.GetProductsPaginQueryHandler request, CancellationToken cancellationToken)
    {
        var productPagedResult = await _productRepository.GetPagedAsync(request.PageIndex, request.PageSize, request.FilterParams, request.SelectedColumns);

        var items = productPagedResult.Items.Select(product => new ProductResponse(
           product.Id,
           product.Name,
           product.Price,
           product.Description
       )).ToList();
        //var result = _mapper.Map<PagedResult<ProductResponse>>(productPagedResult);
        var result = new PagedResult<ProductResponse>(items, productPagedResult.PageIndex, productPagedResult.PageSize, productPagedResult.TotalCount);
        return Result.Success(result);
    }
}
