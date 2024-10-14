namespace PawFund.Contract.Services.Products;

public static class Filter
{
    public record ProductFilter(string? Name, decimal? MinPrice, decimal? MaxPrice);
}
