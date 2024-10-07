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
            public EventNotFoundException(Guid Id) : base($"Can not found event with ID: {Id}")
            {
            }
        }
    }
}
