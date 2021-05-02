using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using MetricsAgent.Responses;
using MetricsAgent.DAL;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu/agent")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;
        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTimePeriod( [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Получение показателей ЦП за период: {fromTime}, {toTime}",
                fromTime.ToString("yyyy-MM-dd"),
                toTime.ToString("yyyy-MM-dd"));

            var result = _repository.GetByTimePeriod(fromTime, toTime);
            if (result is null)
            {
                _logger.LogInformation("По запросу ничего не было найдено.");
                return NotFound();
            }
            _logger.LogInformation("Запрос выполнен успшно.");
            return Ok(new CpuMetricsByTimePeriodResponse() { Metrics = result.Select(val => val.Value).ToList() });
        }
    }
}
