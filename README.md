# Weather App

A modern weather application built with Angular frontend and ASP.NET Core backend with controllers, integrated with OpenWeather API for real-time weather data.

## Features

- 🌤️ **Real-time weather data** from OpenWeather API
- 📍 **Search weather by city name** with instant results
- 🌍 **Popular cities** quick access
- 📅 **7-day weather forecast** with detailed information
- 📱 **Responsive design** for all devices
- 🎨 **Modern UI** with beautiful gradients and glassmorphism
- ⚡ **Fast and efficient** API calls
- 🔄 **Real-time updates** with accurate weather information

## Technology Stack

### Frontend
- **Angular 20** - Modern frontend framework
- **TypeScript** - Type-safe JavaScript
- **CSS3** - Modern styling with gradients and animations
- **RxJS** - Reactive programming

### Backend
- **ASP.NET Core 8** - Modern web framework
- **C#** - Strongly typed programming language
- **Controllers** - RESTful API endpoints
- **Dependency Injection** - Service management
- **OpenWeather API** - Real weather data integration

## Project Structure

```
Weather_App/
├── src/                          # Angular frontend
│   ├── app/
│   │   ├── components/
│   │   │   └── weather/          # Weather component
│   │   ├── services/
│   │   │   └── weather.service.ts # API service
│   │   └── app.*                 # Main app files
├── WeatherApp.API/               # ASP.NET Core backend
│   ├── Controllers/
│   │   └── WeatherController.cs  # Weather API endpoints
│   ├── Models/
│   │   ├── WeatherData.cs        # Data models
│   │   └── OpenWeatherResponse.cs # OpenWeather API models
│   ├── Services/
│   │   ├── WeatherService.cs     # Business logic
│   │   └── OpenWeatherService.cs # OpenWeather API service
│   └── Program.cs                # App configuration
├── start-weather-app.ps1         # Startup script
└── README.md                     # Documentation
```

## Prerequisites

- **OpenWeather API Key** - [Get free API key here](https://openweathermap.org/api)

## Setup Instructions

### 1. Clone and Navigate
```bash
cd Weather_App
```

### 2. Install Frontend Dependencies
```bash
npm install
```

### 3. Configure OpenWeather API Key
The API key is already configured in `WeatherApp.API/appsettings.json`:
```json
{
  "OpenWeatherApi": {
    "BaseUrl": "https://api.openweathermap.org/data/2.5",
    "ApiKey": "YOUR_API_KEY"
  }
}
```

### 4. Build and Run Backend
```bash
cd WeatherApp.API
dotnet restore
dotnet build
dotnet run
```

The API will be available at: `http://localhost:5000`

### 5. Run Frontend (in a new terminal)
```bash
# From the Weather_App directory
npm start
```

The Angular app will be available at: `http://localhost:4200`

## API Endpoints

### Weather Controller (`/api/weather`)

- `GET /api/weather/current/{city}` - Get current weather for a city
- `GET /api/weather/forecast/{city}` - Get 7-day forecast for a city
- `GET /api/weather/cities` - Get list of popular cities
- `POST /api/weather/search` - Search weather by city name

### Example API Calls

```bash
# Get current weather for London
curl http://localhost:5000/api/weather/current/London

# Get forecast for Tokyo
curl http://localhost:5000/api/weather/forecast/Tokyo

# Get popular cities
curl http://localhost:5000/api/weather/cities
```

## Features Overview

### Current Weather Display
- **Real temperature** in Celsius from OpenWeather API
- **"Feels like" temperature** for accurate perception
- **Weather description** (e.g., "overcast clouds", "light rain")
- **Humidity percentage** from real data
- **Wind speed** in m/s
- **Weather icons** mapped to emojis
- **Last updated timestamp**

### 7-Day Forecast
- **Daily temperature** predictions
- **Weather conditions** for each day
- **Visual weather icons** with emoji mapping
- **Responsive grid layout**
- **Accurate forecast data** from OpenWeather

### Search Functionality
- **Search any city** by name worldwide
- **Enter key support** for quick searches
- **Loading states** with spinner
- **Error handling** for invalid cities
- **Real-time validation**

### Popular Cities
- **Quick access** to major cities worldwide
- **Active state indication** for current city
- **Responsive button grid**
- **Updated list** with more cities

## Real Weather Data

This application uses the **OpenWeather API** to provide:
- ✅ **Real-time weather data** for any city worldwide
- ✅ **Accurate temperature** readings in Celsius
- ✅ **Humidity and wind** information
- ✅ **7-day forecast**
- ✅ **Reliable API** with 99.9% uptime

## Development

### Backend Development
```bash
cd WeatherApp.API
dotnet watch run
```

### Frontend Development
```bash
npm run watch
```

## Building for Production

### Backend
```bash
cd WeatherApp.API
dotnet publish -c Release
```

### Frontend
```bash
npm run build
```




### Styling
The app uses modern CSS with:
- CSS Grid and Flexbox
- CSS Custom Properties
- Responsive design
- Smooth animations
- Glassmorphism effects

## Troubleshooting

### CORS Issues
If you encounter CORS errors, ensure the backend is running and the CORS policy is configured correctly in `Program.cs`.

### Port Conflicts
- Backend runs on port 5000 by default
- Frontend runs on port 4200 by default
- Change ports in `launchSettings.json` (backend) or `angular.json` (frontend)

### API Connection
Ensure the API URL in `weather.service.ts` matches your backend URL:
```typescript
private apiUrl = 'http://localhost:5000/api/weather';
```

### OpenWeather API Issues
- Verify your API key is valid
- Check API usage limits (free tier: 1000 calls/day)
- Ensure city names are spelled correctly
