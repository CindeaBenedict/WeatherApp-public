document.addEventListener("DOMContentLoaded", () => {
  const weatherApiKey = "ADD_API_KEY"; // Replace with your valid OpenWeatherMap API key
  const openAiKey = "ADD_API_KEY"; // Replace with your valid OpenAI API key

  const elements = {
    map: document.getElementById("map"),
    weather: document.getElementById("weather-data"),
    forecast: document.getElementById("forecast"),
    locationSearch: document.getElementById("location-search"),
    latitude: document.getElementById("latitude"),
    longitude: document.getElementById("longitude"),
    chatInput: document.getElementById("chat-input"),
    chatMessages: document.getElementById("chat-messages")
  };

  let currentMarker = null;

  // Initialize the map
  const map = L.map("map").setView([46.0, 25.0], 7);

  L.tileLayer("https://{s}.tile.opentopomap.org/{z}/{x}/{y}.png", {
    attribution: "© OpenStreetMap contributors, SRTM | OpenTopoMap style",
    maxZoom: 19,
  }).addTo(map);

  const weatherLayers = {
    Temperature: createWeatherLayer("temp_new"),
    Clouds: createWeatherLayer("clouds_new"),
    Precipitation: createWeatherLayer("precipitation_new"),
    Wind: createWeatherLayer("wind_new"),
    Pressure: createWeatherLayer("pressure_new"),
  };

  weatherLayers.Temperature.addTo(map);

  L.control.layers(null, weatherLayers, {
    collapsed: false,
    position: "topright",
  }).addTo(map);

  L.control.scale().addTo(map);

  // Create weather layers with timestamp to prevent caching
  function createWeatherLayer(layerName) {
    const timestamp = Date.now();
    return L.tileLayer(
      `https://tile.openweathermap.org/map/${layerName}/{z}/{x}/{y}.png?appid=${weatherApiKey}&ts=${timestamp}`,
      {
        attribution: "© OpenWeatherMap",
        maxZoom: 19,
        opacity: 0.7,
        tileSize: 256,
        zIndex: 1,
      }
    );
  }

  // Update weather data
  async function updateWeatherData(latitude, longitude) {
    try {
      const response = await fetch(
        `https://api.openweathermap.org/data/2.5/weather?lat=${latitude}&lon=${longitude}&appid=${weatherApiKey}&units=metric&lang=en`
      );

      if (!response.ok) {
        throw new Error("Weather API request failed");
      }

      const weatherData = await response.json();
      updateDetailedWeather(weatherData);
      await updateForecast(latitude, longitude);

      if (elements.latitude) elements.latitude.textContent = latitude.toFixed(6);
      if (elements.longitude) elements.longitude.textContent = longitude.toFixed(6);
    } catch (error) {
      console.error("Weather data fetch error:", error);
      showError(error.message || "Unable to fetch weather data.");
    }
  }

  // Update detailed weather display
  function updateDetailedWeather(data) {
    if (!elements.weather) return;

    const temp = data.main.temp.toFixed(2);
    const feelsLike = data.main.feels_like.toFixed(2);
    const pressureInMmHg = (data.main.pressure * 0.75006375541921).toFixed(2);
    const windSpeed = (data.wind.speed * 3.6).toFixed(2); // Convert m/s to km/h

    elements.weather.innerHTML = `
      <div class="flex flex-col space-y-6">
        <div class="flex items-center space-x-4">
          <div class="w-10 h-10 rounded-full bg-orange-400 flex items-center justify-center">
            <img src="https://openweathermap.org/img/wn/${data.weather[0].icon}@2x.png" 
                 alt="Weather icon" 
                 class="w-full h-full">
          </div>
          <div>
            <h3 class="text-4xl font-bold">${temp}°C</h3>
            <p class="text-xl text-gray-600">${data.weather[0].description}</p>
          </div>
        </div>
        <div class="grid grid-cols-2 gap-6">
          <div class="bg-gray-50 p-4 rounded-lg">
            <h4 class="text-lg text-gray-600">Detailed Conditions</h4>
            <ul class="space-y-2 mt-2">
              <li>Feels like: ${feelsLike}°C</li>
              <li>Min: ${data.main.temp_min.toFixed(2)}°C</li>
              <li>Max: ${data.main.temp_max.toFixed(2)}°C</li>
              <li>Description: ${data.weather[0].description}</li>
            </ul>
          </div>
          <div class="bg-gray-50 p-4 rounded-lg">
            <h4 class="text-lg text-gray-600">Atmospheric</h4>
            <ul class="space-y-2 mt-2">
              <li>Humidity: ${data.main.humidity}%</li>
              <li>Pressure: ${pressureInMmHg} mmHg</li>
              <li>Visibility: ${(data.visibility / 1000).toFixed(2)} km</li>
              <li>Clouds: ${data.clouds.all}%</li>
            </ul>
          </div>
        </div>
        <div class="text-xs text-gray-500 text-right">
          Last updated: ${new Date().toLocaleTimeString()}
        </div>
      </div>
    `;
  }

  // Update forecast data
  async function updateForecast(latitude, longitude) {
    try {
      const response = await fetch(
        `https://api.openweathermap.org/data/2.5/forecast?lat=${latitude}&lon=${longitude}&appid=${weatherApiKey}&units=metric`
      );

      if (!response.ok) {
        throw new Error("Forecast API request failed");
      }

      const forecastData = await response.json();
      displayForecast(forecastData.list);
    } catch (error) {
      console.error("Forecast data fetch error:", error);
      showError(error.message || "Unable to fetch forecast data.");
    }
  }

  function displayForecast(forecastList) {
    if (!elements.forecast) return;

    const dailyForecasts = forecastList.filter((item) => item.dt_txt.includes("12:00:00"));
    elements.forecast.innerHTML = dailyForecasts
      .slice(0, 7)
      .map(
        (day) => `
        <div class="forecast-item bg-blue-50 p-4 rounded-lg">
          <p class="text-lg font-bold">${new Date(day.dt * 1000).toLocaleDateString()}</p>
          <img src="https://openweathermap.org/img/wn/${day.weather[0].icon}@2x.png" 
               alt="Weather icon" 
               class="w-32 h-32 mx-auto">
          <p>${day.weather[0].description}</p>
          <p>Temp: ${day.main.temp.toFixed(1)}°C</p>
          <p>Wind: ${(day.wind.speed * 3.6).toFixed(1)} km/h</p>
        </div>
      `
      )
      .join("");
  }

    // Initial weather data update with fallback location
    updateWeatherData(46.0, 25.0);
  });