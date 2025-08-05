using System.Text.Json.Serialization;

namespace WeatherApp.API.Models
{
    public class OpenWeatherCurrentResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("main")]
        public MainWeather Main { get; set; } = new();

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; } = new();

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; } = new();

        [JsonPropertyName("dt")]
        public long Timestamp { get; set; }
    }

    public class OpenWeatherForecastResponse
    {
        [JsonPropertyName("list")]
        public List<ForecastItem> List { get; set; } = new();

        [JsonPropertyName("city")]
        public City City { get; set; } = new();
    }

    public class MainWeather
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;
    }

    public class Wind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
    }

    public class ForecastItem
    {
        [JsonPropertyName("dt")]
        public long Timestamp { get; set; }

        [JsonPropertyName("main")]
        public MainWeather Main { get; set; } = new();

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; } = new();

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; } = new();
    }

    public class City
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
} 