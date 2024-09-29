
namespace PawFund.Domain.Exceptions
{
    public static class BranchException
    {
        public class BranchNotFoundException : NotFoundException
        {
            public BranchNotFoundException(string message) : base(message)
            {
            }
        }
    }
}
