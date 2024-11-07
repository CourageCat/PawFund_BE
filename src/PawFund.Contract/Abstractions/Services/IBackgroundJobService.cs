
using System.Linq.Expressions;

namespace PawFund.Contract.Abstractions.Services
{
    public interface IBackgroundJobService
    {
        void ScheduleRecurringJob(string jobId, Expression<Func<Task>> methodCall, string cronExpression);
    }
}
