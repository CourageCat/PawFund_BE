using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.Donors
{
    public static class Command
    {
        public record CreateDonationCommand(Guid id, decimal amount, string description, Guid PaymentMethodId) : Abstractions.Message.ICommand;

    }
}
