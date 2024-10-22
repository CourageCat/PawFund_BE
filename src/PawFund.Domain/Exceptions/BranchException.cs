using PawFund.Contract.Enumarations.MessagesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Domain.Exceptions
{
    public static class BranchException
    {
        public class BranchNotFoundException : NotFoundException
        {
            public BranchNotFoundException(Guid Id) : base(string.Format(MessagesList.BranchNotFoundException.GetMessage().Message, Id), MessagesList.BranchNotFoundException.GetMessage().Code)
            { }
        }
    }
}
