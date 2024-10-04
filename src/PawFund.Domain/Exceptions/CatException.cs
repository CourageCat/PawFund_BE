using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class CatException
    {
        public class CatNotFoundException : NotFoundException
        {
            public CatNotFoundException(Guid Id) : base($"Can not found Cat with Id: {Id}")
            {
            }
        }
    }
}
