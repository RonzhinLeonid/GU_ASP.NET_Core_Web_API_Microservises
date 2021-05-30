using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL.Interfaces;
using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentsRepository _repository;
        private readonly ILogger<AgentsController> _logger;
        private readonly IMapper _mapper;

        public AgentsController(IAgentsRepository repository, ILogger<AgentsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            try
            {
                _repository.Create(_mapper.Map<AgentInfo>(agentInfo));
                _logger.LogInformation("Регистрация агента");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Не удалось зарегистрировать агента");
                return StatusCode(500);
            }
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            try
            {
                var agent = _repository.GetById(agentId);
                if (agent is null)
                {
                    return NotFound();
                }
                agent.IsEnabled = true;
                _repository.Update(agent);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Не удалось активировать агента");
                return StatusCode(500);
            }
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            try
            {
                var agent = _repository.GetById(agentId);
                if (agent is null)
                {
                    return NotFound();
                }
                agent.IsEnabled = false;
                _repository.Update(agent);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Не удалось деактивировать агента");
                return StatusCode(500);
            }
        }

        [HttpGet("getregistagents")]
        public IActionResult GetRegisterAgents()
        {
            var agents = _repository.Get();
            return Ok(new GetRegisteredAgentsResponse()
            {
                Agents = agents.Select(_mapper.Map<AgentsResponse>)
            });
        }
    }
}
