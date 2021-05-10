using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using MetricsAgent.Responses;
using MetricsAgent.DAL;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricsController> _logger;
        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
        }
        [HttpGet("available")]
        public IActionResult GetAvailable([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
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
            return Ok(new NetworkMetricsByTimePeriodResponse() { Metrics = result.Select(val => val.Value).ToList() });
        }
    }
}
