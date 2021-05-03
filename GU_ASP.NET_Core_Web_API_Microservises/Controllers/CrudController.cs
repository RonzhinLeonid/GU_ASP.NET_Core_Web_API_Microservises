using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GU_ASP.NET_Core_Web_API_Microservises.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly WeatherForecast _holder;

        public CrudController(WeatherForecast holder)
        {
            _holder = holder;
        }

        [HttpPost("create")]
        public IActionResult CreateTemperature([FromQuery] DateTime? date, [FromQuery] int temperature)
        {
            try
            {
                if (date == null) return StatusCode(400);
                _holder.AddTemperature((DateTime)date, temperature);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch 
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult UpdateTemperature([FromQuery] DateTime? date, [FromQuery] int? temperature)
        {
            try
            {
                if (date == null || temperature == null) return StatusCode(400);
                _holder.UpdateTemperature((DateTime)date, (int)temperature);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult DeleteRangeTemperature([FromQuery] DateTime? beginTime, [FromQuery] DateTime? endTime)
        {
            try
            {
                if (beginTime == null || endTime == null) return StatusCode(400);
                _holder.deleteRangeTemperature((DateTime)beginTime, (DateTime)endTime);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpGet("read")]
        public IEnumerable<Weather> GetTemperatures([FromQuery] DateTime beginTime, [FromQuery] DateTime endTime)
        {
            var weather = _holder.GetTemperatures(beginTime, endTime);
            return weather;
        }

        [HttpGet("readAll")]
        public IEnumerable<Weather> GetAllTemperatures()
        {
            var weather = _holder.GetAllTemperatures();
            return weather;
        }
    }
}
