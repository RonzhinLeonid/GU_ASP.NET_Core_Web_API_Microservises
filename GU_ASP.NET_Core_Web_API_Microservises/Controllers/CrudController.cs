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
        public IActionResult CreateTemperature([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            try
            {
                _holder.AddTemperature(date, temperature);
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
        public IActionResult UpdateTemperature([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            try
            {
                _holder.UpdateTemperature(date, temperature);
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
        public IActionResult DeleteRangeTemperature([FromQuery] DateTime beginTime, [FromQuery] DateTime endTime)
        {
            try
            {
                _holder.deleteRangeTemperature(beginTime, endTime);
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
