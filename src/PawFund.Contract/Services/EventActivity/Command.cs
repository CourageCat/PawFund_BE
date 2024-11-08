using PawFund.Contract.Abstractions.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Contract.Services.EventActivity
{
    public static class Command
    {
        public record CreateEventActivityCommand(string Name, int Quantity, DateTime StartDate, string Description, Guid EventId) : ICommand;
        public record UpdateEventActivityCommand(Guid Id, string Name, int Quantity, DateTime StartDate, string Description, bool Status, Guid EventId) : ICommand;
        public record DeleteEventActivityCommand(Guid Id) : ICommand;
    }
}
