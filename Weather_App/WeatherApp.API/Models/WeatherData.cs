namespace WeatherApp.API.Models
{
    public class WeatherData
    {
        public string City { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public double WindSpeed { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class WeatherForecast
    {
        public string City { get; set; } = string.Empty;
        public List<WeatherData> threeHourForecast { get; set; } = new List<WeatherData>(); // 3-hour interval forecast for up to 5 days
    }

    public class WeatherRequest
    {
        public string City { get; set; } = string.Empty;
    }
} 