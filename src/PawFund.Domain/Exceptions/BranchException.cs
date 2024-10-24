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
            public BranchNotFoundException(Guid Id) : base(string.Format(MessagesList.BranchNotFoundBranchException.GetMessage().Message, Id), MessagesList.BranchNotFoundBranchException.GetMessage().Code)
            {
            }
        }

        public class BranchEmptyException : NotFoundException
        {
            public BranchEmptyException() : base((MessagesList.BranchEmptyBranchesException.GetMessage().Message), MessagesList.BranchEmptyBranchesException.GetMessage().Code)
            {
            }
        }

        public class BranchNotFoundOfStaffException : NotFoundException
        {
            public BranchNotFoundOfStaffException(Guid Id) : base(string.Format(MessagesList.BranchNotFoundBranchOfStaffException.GetMessage().Message, Id), MessagesList.BranchNotFoundBranchOfStaffException.GetMessage().Code)
            {
            }
        }
    }
}
