using PawFund.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PawFund.Persistence;
public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<Product> Products { get; set; }
}