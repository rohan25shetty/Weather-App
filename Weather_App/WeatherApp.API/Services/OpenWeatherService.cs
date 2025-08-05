using System.Text.Json;
using WeatherApp.API.Models;

namespace WeatherApp.API.Services
{
    public interface IOpenWeatherService
    {
        Task<OpenWeatherCurrentResponse> GetCurrentWeatherAsync(string city);
        Task<OpenWeatherForecastResponse> GetForecastAsync(string city);
    }

    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OpenWeatherService> _logger;

        public OpenWeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<OpenWeatherService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<OpenWeatherCurrentResponse> GetCurrentWeatherAsync(string city)
        {
            try
            {
                var apiKey = _configuration["OpenWeatherApi:ApiKey"];
                var baseUrl = _configuration["OpenWeatherApi:BaseUrl"];
                
                var url = $"{baseUrl}/weather?q={Uri.EscapeDataString(city)}&appid={apiKey}&units=metric";
                
                _logger.LogInformation("Fetching current weather for {City}", city);
                
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<OpenWeatherCurrentResponse>(content);
                
                return weatherData ?? new OpenWeatherCurrentResponse();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching current weather for {City}", city);
                throw new Exception($"Failed to fetch weather data for {city}. Please check if the city name is correct.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error fetching current weather for {City}", city);
                throw;
            }
        }

        public async Task<OpenWeatherForecastResponse> GetForecastAsync(string city)
        {
            try
            {
                var apiKey = _configuration["OpenWeatherApi:ApiKey"];
                var baseUrl = _configuration["OpenWeatherApi:BaseUrl"];
                
                var url = $"{baseUrl}/forecast?q={Uri.EscapeDataString(city)}&appid={apiKey}&units=metric";
                
                _logger.LogInformation("Fetching forecast for {City}", city);
                
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var forecastData = JsonSerializer.Deserialize<OpenWeatherForecastResponse>(content);
                
                return forecastData ?? new OpenWeatherForecastResponse();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching forecast for {City}", city);
                throw new Exception($"Failed to fetch forecast data for {city}. Please check if the city name is correct.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error fetching forecast for {City}", city);
                throw;
            }
        }
    }
} 