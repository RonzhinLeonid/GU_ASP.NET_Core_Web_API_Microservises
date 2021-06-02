using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;
using MetricsManager.Services;
using MetricsManager.Services.Requests;
using Quartz;


namespace MetricsManager.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;
        private readonly IAgentsRepository _agentsRepository;
        private readonly IMetricsAgentClient _client;
        public CpuMetricJob(ICpuMetricsRepository repository, IAgentsRepository agentsRepository, IMetricsAgentClient client)
        {
            _repository = repository;
            _agentsRepository = agentsRepository;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var agents = _agentsRepository.Get();
            foreach (var agent in agents)
            {
                var metricsList = _client.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest
                {
                    FromTime = _repository.GetAgentLastMetricDate(agent.Id),
                    ToTime = DateTimeOffset.UtcNow,
                    AgentUrl = agent.Url
                });
                if (!object.ReferenceEquals(metricsList, null))
                {
                    foreach (var metric in metricsList.Metrics)
                    {
                        _repository.Create(metric);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
