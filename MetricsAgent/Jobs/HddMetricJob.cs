using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;
        private readonly PerformanceCounter _counter;
        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter("LogicalDisk", "Free Megabytes", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddUsageInPercents = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new HddMetrics(){ Time = time, Value = hddUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
