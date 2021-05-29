using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class DotnetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private readonly PerformanceCounter _counter;
        public DotnetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter(".NET CLR Exceptions", "# of Exceps Thrown", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var dotnetUsageInPercents = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new DotNetMetrics(){ Time = time, Value = dotnetUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
