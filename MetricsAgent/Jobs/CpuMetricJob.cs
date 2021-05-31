using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;
        private readonly PerformanceCounter _counter;
        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new CpuMetric (){ Time = time, Value = cpuUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
