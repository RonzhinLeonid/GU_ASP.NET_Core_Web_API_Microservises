using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using MetricsAgent.Responses;
using AutoMapper;
using MetricsAgent.DAL;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkMetricsController> _logger;
        private IMapper _mapper;
        public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Получение метрик за период: {fromTime}, {toTime}",
                fromTime.ToString("yyyy-MM-dd"),
                toTime.ToString("yyyy-MM-dd"));

            var result = _repository.GetByTimePeriod(fromTime, toTime);
            if (result is null)
            {
                _logger.LogInformation("По запросу ничего не было найдено.");
                return NotFound();
            }
            _logger.LogInformation("Запрос выполнен успшно.");
            return Ok(new NetworkMetricsByTimePeriodResponse() { Metrics = result.Select(_mapper.Map<NetworkMetricsResponse>).ToList() });
        }
    }
}
