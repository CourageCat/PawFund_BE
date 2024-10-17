using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class HistoryCatException
    {
        public class HistoryCatNotFoundException : NotFoundException
        {
            public HistoryCatNotFoundException(Guid Id) : base($"Can not found History Cat with Id: {Id}")
            {
            }
        }
    }
}
