using Microsoft.Extensions.Configuration;
using PawFund.Contract.Abstractions.Services;
using PawFund.Contract.Enumarations.Authentication;
using PawFund.Contract.Enumarations.PaymentMethod;
using PawFund.Domain.Entities;

namespace PawFund.Persistence.SeedData;

public static class SeedData
{
    public static void Seed(ApplicationDbContext context, IConfiguration configuration, IPasswordHashService passwordHashService)
    {
        if (!context.RoleUsers.Any())
        {
            context.RoleUsers.AddRange(
                new RoleUser
                {
                    Id = RoleType.Admin,
                    RoleName = "Admin",
                },
                new RoleUser
                {
                    Id = RoleType.Staff,
                    RoleName = "Staff",
                },
                new RoleUser
                {
                    Id = RoleType.Member,
                    RoleName = "Member"
                }
            );
        }

        if (!context.PaymentMethods.Any())
        {
            context.PaymentMethods.AddRange(
                new PaymentMethod
                {
                    Id = PaymentMethodType.Cash,
                    MethodName = "Cash",
                },
                new PaymentMethod
                {
                    Id = PaymentMethodType.Banking,
                    MethodName = "Banking"
                }
            );
        }

        if (!context.Accounts.Any())
        {
            context.Accounts.AddRange(
                Account.CreateAdminAccount(configuration["AccountAdmin:Email"], passwordHashService.HashPassword(configuration["AccountAdmin:Password"]))
            );
        }
        context.SaveChanges();
    }
}
