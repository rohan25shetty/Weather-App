using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Models;
using WeatherApp.API.Services;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("current/{city}")]
        public async Task<ActionResult<WeatherData>> GetCurrentWeather(string city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city))
                {
                    return BadRequest("City name is required");
                }

                var weather = await _weatherService.GetCurrentWeatherAsync(city);
                return Ok(weather);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("forecast/{city}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(string city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city))
                {
                    return BadRequest("City name is required");
                }

                var forecast = await _weatherService.GetWeatherForecastAsync(city);
                return Ok(forecast);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("cities")]
        public async Task<ActionResult<List<string>>> GetPopularCities()
        {
            try
            {
                var cities = await _weatherService.GetPopularCitiesAsync();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("search")]
        public async Task<ActionResult<WeatherData>> SearchWeather([FromBody] WeatherRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.City))
                {
                    return BadRequest("City name is required");
                }

                var weather = await _weatherService.GetCurrentWeatherAsync(request.City);
                return Ok(weather);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 