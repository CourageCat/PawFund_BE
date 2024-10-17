using Microsoft.AspNetCore.Http;
using PawFund.Contract.Enumarations.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawFund.Contract.Services.Accounts
{
    public static class Command
    {
        public record UpdateUserCommand(Guid ID, string FirstName, string LastName, IFormFile? AvatarFile, string Password) : Abstractions.Message.ICommand;

    }
}
