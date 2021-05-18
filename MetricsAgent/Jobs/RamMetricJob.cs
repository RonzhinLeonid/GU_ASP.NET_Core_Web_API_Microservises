using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private readonly PerformanceCounter _counter;
        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var ramUsageInPercents = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new RamMetrics(){ Time = time, Value = ramUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
