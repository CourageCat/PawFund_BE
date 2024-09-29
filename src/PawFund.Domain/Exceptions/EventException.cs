using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class EventException
    {
        public class EventNotFoundException : NotFoundException
        {
            public EventNotFoundException(string message) : base(message)
            {
            }
        }

        public class EventIsDeletedException : BadRequestException
        {
            public EventIsDeletedException(string message) : base(message)
            {
            }
        }
    }
}
