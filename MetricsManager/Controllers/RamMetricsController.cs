using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL.Interfaces;
using AutoMapper;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMapper _mapper;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            _mapper = mapper;
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
