using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("left/agent/{agentId}")]
        public IActionResult GetFreeHDDSpaceFromAgent([FromRoute] int agentId)
        {
            _logger.LogInformation("Получение свободного места на HDD у {agentId}",
                agentId);
            return Ok();
        }
        [HttpGet("left/cluster")]
        public IActionResult GetFreeHDDSpaceFromAllCluster()
        {
            _logger.LogInformation("Получение свободного места на HDD");
            return Ok();
        }
    }
}
