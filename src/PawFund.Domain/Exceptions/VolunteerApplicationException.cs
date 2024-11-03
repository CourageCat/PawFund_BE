using PawFund.Contract.Enumarations.MessagesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class VolunteerApplicationException
    {
        public class VolunteerApplicationMaximumException : BadRequestException
        {
            public VolunteerApplicationMaximumException() : base(MessagesList.VolunteerApplicationMaximumException.GetMessage().Message, MessagesList.VolunteerApplicationMaximumException.GetMessage().Code)
            { }
        }

        public class VolunteerApplicationAlreadyRegistException : BadRequestException
        {
            public VolunteerApplicationAlreadyRegistException() : base(MessagesList.VolunteerApplicationAlreadyRegistException.GetMessage().Message, MessagesList.VolunteerApplicationAlreadyRegistException.GetMessage().Code)
            { }
        }
    }
}
