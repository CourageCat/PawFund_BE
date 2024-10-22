using PawFund.Contract.Enumarations.MessagesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class VolunteerApplicationDetailException
    {
        public class CheckVolunteerApplicationException : BadRequestException
        {
            public CheckVolunteerApplicationException() : base(MessagesList.CheckVolunteerApplicationException.GetMessage().Message, MessagesList.CheckVolunteerApplicationException.GetMessage().Code)
            { }
        }

        public class VolunteerApplicationNotFoundException : NotFoundException
        {
            public VolunteerApplicationNotFoundException(Guid Id) : base(string.Format(MessagesList.VolunteerApplicationNotFoundException.GetMessage().Message, Id), MessagesList.VolunteerApplicationNotFoundException.GetMessage().Code)
            { }
        }
    }
}
