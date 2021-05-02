using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("available/agent/{agentId}")]
        public IActionResult GetAvailableFromAgent([FromRoute] int agentId)
        {
            _logger.LogInformation("Получение RAM у {agentId}",
                agentId);
            return Ok();
        }
        [HttpGet("available/cluster")]
        public IActionResult GetAvailableFromAllCluster()
        {
            _logger.LogInformation("Получение RAM");
            return Ok();
        }
    }
}
