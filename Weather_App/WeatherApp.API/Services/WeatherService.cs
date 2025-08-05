using WeatherApp.API.Models;

namespace WeatherApp.API.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetCurrentWeatherAsync(string city);
        Task<WeatherForecast> GetWeatherForecastAsync(string city);
        Task<List<string>> GetPopularCitiesAsync();
    }

    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherService _openWeatherService;
        private readonly ILogger<WeatherService> _logger;
        private readonly IConfiguration _configuration;

        public WeatherService(IOpenWeatherService openWeatherService, ILogger<WeatherService> logger, IConfiguration configuration)
        {
            _openWeatherService = openWeatherService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<WeatherData> GetCurrentWeatherAsync(string city)
        {
            try
            {
                var openWeatherData = await _openWeatherService.GetCurrentWeatherAsync(city);
                
                return new WeatherData
                {
                    City = openWeatherData.Name,
                    Temperature = Math.Round(openWeatherData.Main.Temp, 1),
                    FeelsLike = Math.Round(openWeatherData.Main.FeelsLike, 1),
                    Humidity = openWeatherData.Main.Humidity,
                    Description = openWeatherData.Weather.FirstOrDefault()?.Description ?? "Unknown",
                    Icon = openWeatherData.Weather.FirstOrDefault()?.Icon ?? "01d",
                    WindSpeed = Math.Round(openWeatherData.Wind.Speed, 1),
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(openWeatherData.Timestamp).UtcDateTime
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current weather for {City}", city);
                throw;
            }
        }

        public async Task<WeatherForecast> GetWeatherForecastAsync(string city)
        {
            try
            {
                var openWeatherForecast = await _openWeatherService.GetForecastAsync(city);
                var forecast = new WeatherForecast
                {
                    City = openWeatherForecast.City.Name,
                    threeHourForecast = new List<WeatherData>()
                };
                
                _logger.LogInformation("Processing forecast for {City}, {Count} items", city, openWeatherForecast.List.Count);
                
                // Take up to 40 items (5 days, 3-hour intervals)
                foreach (var item in openWeatherForecast.List.Take(40))
                {
                    var timestamp = DateTimeOffset.FromUnixTimeSeconds(item.Timestamp).UtcDateTime;
                    forecast.threeHourForecast.Add(new WeatherData
                    {
                        City = openWeatherForecast.City.Name,
                        Temperature = Math.Round(item.Main.Temp, 1),
                        FeelsLike = Math.Round(item.Main.FeelsLike, 1),
                        Humidity = item.Main.Humidity,
                        Description = item.Weather.FirstOrDefault()?.Description ?? "Unknown",
                        Icon = item.Weather.FirstOrDefault()?.Icon ?? "01d",
                        WindSpeed = Math.Round(item.Wind.Speed, 1),
                        Timestamp = timestamp
                    });
                }
                
                _logger.LogInformation("Returning {Count} forecast items for {City}", forecast.threeHourForecast.Count, city);
                return forecast;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting forecast for {City}", city);
                throw;
            }
        }

        public Task<List<string>> GetPopularCitiesAsync()
        {
            // Get popular cities from configuration
            var popularCities = _configuration.GetSection("PopularCities").Get<List<string>>();

            // Return the list, or an empty list if not found in configuration
            return Task.FromResult(popularCities ?? new List<string>());
        }
    }
} 