
using Hangfire;
using PawFund.Contract.Abstractions.Services;
using System.Linq.Expressions;

namespace PawFund.Infrastructure.Worker
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public void ScheduleRecurringJob(string jobId, Expression<Func<Task>> methodCall, string cronExpression)
        {
            // Sử dụng Hangfire để lên lịch job định kỳ
            RecurringJob.AddOrUpdate(jobId, methodCall, cronExpression);
        }
    }
}
