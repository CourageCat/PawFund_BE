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
            public EventActivityNotFoundException(Guid Id) : base($"Can not found event activity with ID: {Id}")
            {
            }
        }
    }
}
