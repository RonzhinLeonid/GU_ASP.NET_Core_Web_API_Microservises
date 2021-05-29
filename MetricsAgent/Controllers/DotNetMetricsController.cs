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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly ILogger<DotNetMetricsController> _logger;
        private IMapper _mapper;
        public DotNetMetricsController(IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCount([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Получение ошибок за период: {fromTime}, {toTime}",
                fromTime.ToString("yyyy-MM-dd"),
                toTime.ToString("yyyy-MM-dd"));

            var result = _repository.GetByTimePeriod(fromTime, toTime);
            if (result is null)
            {
                _logger.LogInformation("По запросу ничего не было найдено.");
                return NotFound();
            }
            _logger.LogInformation("Запрос выполнен успшно.");
            return Ok(new DotNetMetricsByTimePeriodResponse() { Metrics = result.Select(_mapper.Map<DotNetMetricsResponse>).ToList() });
        }
    }
}
