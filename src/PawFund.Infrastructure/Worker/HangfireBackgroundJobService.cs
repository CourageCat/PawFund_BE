using Hangfire;
using PawFund.Contract.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PawFund.Infrastructure.Worker
{
    class HangfireBackgroundJobService : IBackgroundJobService
    {
        public void ScheduleRecurringJob(string jobId, Expression<Func<Task>> methodCall, string cronExpression)
        {
            // Sử dụng Hangfire để lên lịch job định kỳ
            RecurringJob.AddOrUpdate(jobId, methodCall, cronExpression);
        }
    }
}
