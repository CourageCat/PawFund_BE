using PawFund.Contract.Enumarations.MessagesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public class EventActivityException
    {
        public class EventActivityNotFoundException : NotFoundException
        {
            public EventActivityNotFoundException(Guid Id) : base(string.Format(MessagesList.EventActivityNotFoundException.GetMessage().Message, Id), MessagesList.EventActivityNotFoundException.GetMessage().Code)
            { }
        }
    }
}
