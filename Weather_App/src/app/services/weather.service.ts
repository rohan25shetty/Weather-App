import { Injectable, Injector, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface WeatherData {
  city: string;
  temperature: number;
  feelsLike: number;
  humidity: number;
  description: string;
  icon: string;
  windSpeed: number;
  timestamp: string;
}

export interface WeatherForecast {
  city: string;
  threeHourForecast: WeatherData[];
}

export interface WeatherRequest {
  city: string;
}

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  private apiUrl = 'http://localhost:5000/api/weather';

  constructor(private http: HttpClient) {}


  getCurrentWeather(city: string): Observable<WeatherData> {
    const encodedCity = encodeURIComponent(city);
    return this.http.get<WeatherData>(`${this.apiUrl}/current/${encodedCity}`);
  }

  getWeatherForecast(city: string): Observable<WeatherForecast> {
    const encodedCity = encodeURIComponent(city);
    return this.http.get<WeatherForecast>(`${this.apiUrl}/forecast/${encodedCity}`);
  }

  getPopularCities(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/cities`);
  }

  searchWeather(request: WeatherRequest): Observable<WeatherData> {
    return this.http.post<WeatherData>(`${this.apiUrl}/search`, request);
  }
} 