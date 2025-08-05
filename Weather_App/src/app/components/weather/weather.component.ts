import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WeatherService, WeatherData, WeatherForecast } from '../../services/weather.service';

@Component({
  selector: 'app-weather',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent implements OnInit {
  currentWeather: WeatherData | null = null;
  forecast: WeatherForecast | null = null;
  popularCities: string[] = [];
  searchCity: string = '';
  loading = false;
  error: string = '';

  constructor(private weatherService: WeatherService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.loadPopularCities();
    // Default to London
    this.searchWeather('Bengaluru');
  }

  loadPopularCities(): void {
    this.weatherService.getPopularCities().subscribe({
      next: (cities) => {
        this.popularCities = cities;
      },
      error: (error) => {
        console.error('Error loading popular cities:', error);
        this.error = 'Failed to load popular cities';
      }
    });
  }

  searchWeather(city: string): void {
    this.weatherService.getCurrentWeather(city).subscribe({
      next: (weather) => {
        this.currentWeather = { ...weather };
        setTimeout(() => this.cdr.detectChanges());  // ✅ Defer change detection
        this.loadForecast(city);
      },
      error: (error) => {
        console.error('Error fetching weather:', error);
        this.error = error.message || 'Failed to fetch weather data.';
        this.loading = false;
      }
    });
  }

  loadForecast(city: string): void {
    this.weatherService.getWeatherForecast(city).subscribe({
      next: (forecast) => {
        this.forecast = { ...forecast };
        this.loading = false;
        setTimeout(() => this.cdr.detectChanges());  // ✅ Defer here too
      },
      error: (error) => {
        console.error('Error fetching forecast:', error);
        this.loading = false;
      }
    });
  }

  // Fetch weather by coordinates (reverse geocode to city, then fetch)
  fetchWeatherByCoords(lat: number, lon: number): void {
    this.loading = true;
    this.error = '';
    // Use OpenWeatherMap reverse geocoding API (or similar)
    fetch(`https://api.openweathermap.org/geo/1.0/reverse?lat=${lat}&lon=${lon}&limit=1&appid="a29cc98bf2928cca3cbead02c8298214"`)
      .then(res => res.json())
      .then(data => {
        if (data && data.length > 0 && data[0].name) {
          this.searchWeather(data[0].name);
        } else {
          this.error = 'Could not determine city from your location.';
          this.loading = false;
        }
      })
      .catch(() => {
        this.error = 'Failed to get city from your location.';
        this.loading = false;
      });
  }

  getCurrentLocationWeather(): void {
    if (!navigator.geolocation) {
      this.error = 'Geolocation is not supported by your browser.';
      return;
    }
    this.loading = true;
    navigator.geolocation.getCurrentPosition(
      (position) => {
        this.fetchWeatherByCoords(position.coords.latitude, position.coords.longitude);
      },
      (err) => {
        this.error = 'Unable to retrieve your location.';
        this.loading = false;
      }
    );
  }

  onSearch(): void {
    if (this.searchCity.trim()) {
      this.searchWeather(this.searchCity);
    }
  }

  selectCity(city: string): void {
    this.currentWeather = null;
    this.forecast = null;
    this.searchCity = city; // Update the search field with the selected city
    this.searchWeather(city);
  }

  // Utility: Get daily summary from 3-hour forecast (closest to noon for each day)
  getDailyForecast(threeHourForecast: WeatherData[]): WeatherData[] {
    const grouped: { [date: string]: WeatherData[] } = {};
    threeHourForecast.forEach(item => {
      try {
        const date = new Date(item.timestamp).toDateString();
        if (!grouped[date]) grouped[date] = [];
        grouped[date].push(item);
      } catch (error) {
        console.error('Error parsing timestamp:', item.timestamp, error);
      }
    });
    // For each day, pick the forecast closest to 12:00
    return Object.values(grouped).map(items =>
      items.reduce((prev, curr) => {
        try {
          const prevHours = new Date(prev.timestamp).getHours();
          const currHours = new Date(curr.timestamp).getHours();
          return Math.abs(currHours - 12) < Math.abs(prevHours - 12) ? curr : prev;
        } catch (error) {
          console.error('Error comparing timestamps:', error);
          return prev;
        }
      })
    );
  }

  getWeatherIcon(icon: string): string {
    // Map OpenWeather icons to emoji
    const iconMap: { [key: string]: string } = {
      // Clear sky
      '01d': '☀️', '01n': '🌙',
      // Few clouds
      '02d': '⛅', '02n': '☁️',
      // Scattered clouds
      '03d': '☁️', '03n': '☁️',
      // Broken clouds
      '04d': '☁️', '04n': '☁️',
      // Shower rain
      '09d': '🌧️', '09n': '🌧️',
      // Rain
      '10d': '🌦️', '10n': '🌧️',
      // Thunderstorm
      '11d': '⛈️', '11n': '⛈️',
      // Snow
      '13d': '❄️', '13n': '❄️',
      // Mist
      '50d': '🌫️', '50n': '🌫️'
    };
    return iconMap[icon] || '🌤️';
  }

  getTemperatureColor(temp: number): string {
    if (temp < 10) return 'cold';
    if (temp > 25) return 'hot';
    return 'warm';
  }

  getWeatherDescription(description: string): string {
    // Capitalize first letter of each word
    return description.split(' ')
      .map(word => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ');
  }

  clearError(): void {
    this.error = '';
  }
} 