using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Responses;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            _mapper = mapper;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Получение показателей ЦП за период: {fromTime}, {toTime} от {agentId}",
                agentId,
                fromTime.ToString(),
                toTime.ToString());
            var result = _repository.GetByTimePeriod(fromTime, toTime, agentId);

            return Ok(new CpuGetMetricsFromAgentResponse()
            {
                Metrics = result.Select(_mapper.Map<CpuMetricResponse>)
            });
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Получение показателей ЦП за период: {fromTime}, {toTime}",
                fromTime.ToString(),
                toTime.ToString());
            var result = _repository.GetByTimePeriod(fromTime, toTime);

            return Ok(new CpuGetMetricsFromAllClusterResponse()
            {
                Metrics = result.Select(_mapper.Map<CpuMetricResponse>)
            });
        }
    }
}
