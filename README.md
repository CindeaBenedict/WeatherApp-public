# WeatherApp 🌦️

WeatherApp is a modern, responsive weather application that displays real-time weather data and a 7-day forecast using the OpenWeatherMap API. It includes a map to show your current location and dynamically adjusts its content based on whether the user is logged in or not.

---

## Features 🚀
- 🌍 Displays your current location on a map.
- 🌤️ Shows the current weather conditions.
- 📅 Provides a 7-day forecast for logged-in users.
- 🔑 User authentication with login and register functionality.
- 🖌️ Beautiful, responsive design using **Tailwind CSS**.

---

## Requirements ✅
1. **.NET SDK** (6.0 or newer)
2. **Node.js** (for Tailwind CSS)
3. **SQL Database** (e.g., SQLite, SQL Server)
4. **OpenWeatherMap API Key** ([Sign up here](https://openweathermap.org/api))
5. **Leaflet.js** for map rendering (integrated).

---

## Getting Started 💻

Follow these steps to set up the project locally:

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/weatherapp.git
cd weatherapp
```

### 2. Install .NET Dependencies
Ensure you have the .NET SDK installed. Install the required NuGet packages:
```bash
dotnet restore
```

### 3. Install Tailwind CSS
Tailwind CSS is used for styling. Install Tailwind and its dependencies:
```bash
npm install -D tailwindcss postcss autoprefixer
npx tailwindcss init
```

### 4. Set Up Tailwind CSS
Edit `tailwind.config.js` to include your project files:
```javascript
module.exports = {
  content: ["./Pages/**/*.cshtml", "./wwwroot/**/*.js"],
  theme: {
    extend: {},
  },
  plugins: [],
};
```

Add the following to your `wwwroot/css/styles.css`:
```css
@tailwind base;
@tailwind components;
@tailwind utilities;
```

Build the Tailwind CSS output:
```bash
npx tailwindcss -i ./wwwroot/css/styles.css -o ./wwwroot/css/output.css --watch
```

### 5. Configure the Database
1. Open `appsettings.json` and configure your database connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=WeatherApp.db"
     }
   }
   ```
2. Add the database migrations and apply them:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

### 6. Set Up OpenWeatherMap API
1. Go to [OpenWeatherMap API](https://openweathermap.org/api) and sign up for a free account.
2. Copy your API key and paste it in `wwwroot/js/main.js`:
   ```javascript
   const weatherApiKey = "YOUR_API_KEY_HERE";
   ```

### 7. Run the Application
Run the application locally:
```bash
dotnet run
```
The application will be available at `http://localhost:5000` (or another port specified in the terminal).

---

## Project Structure 🖂️
```
WeatherApp/
├── Pages/
│   ├── Index.cshtml           # Main page (dynamic based on login status)
│   ├── Shared/
│   │   ├── _Header.cshtml     # Shared header component
│   │   ├── _Footer.cshtml     # Shared footer component
├── wwwroot/
│   ├── css/
│   │   ├── styles.css         # Tailwind input file
│   │   ├── output.css         # Compiled CSS
│   ├── js/
│   │   ├── main.js            # JavaScript for geolocation and weather data
├── appsettings.json           # Configuration file (database connection)
├── Program.cs                 # Entry point for the application
├── Startup.cs                 # Optional if using .NET 5
```

---

## How It Works ⚙️

1. **Home Page for Guests:**
   - Displays a map with the user's location.
   - Shows basic weather data (current temperature, description).

2. **Home Page for Logged-In Users:**
   - Displays additional weather data (7-day forecast).
   - Shows personalized greetings using the logged-in user's name.

3. **Authentication:**
   - Users can register an account (name, email, username, password).
   - Users can log in to access personalized features.

4. **Real-Time Weather Data:**
   - Weather data is fetched from the OpenWeatherMap API using geolocation coordinates.

5. **Responsive Design:**
   - Styled with Tailwind CSS to look great on all devices.

---

## Screenshots 🌟

### Guest View
![Guest View](link-to-image)

### Logged-In View
![Logged-In View](link-to-image)

---

## Troubleshooting 🔧

1. **`dotnet ef` not found:**
   - Install the EF Core tools globally:
     ```bash
     dotnet tool install --global dotnet-ef
     ```

2. **CSS not updating:**
   - Ensure Tailwind is running in watch mode:
     ```bash
     npx tailwindcss -i ./wwwroot/css/styles.css -o ./wwwroot/css/output.css --watch
     ```

3. **Geolocation not working:**
   - Ensure you’ve allowed location access in your browser.

4. **API errors:**
   - Double-check your OpenWeatherMap API key.

---

## Contributing 🤝
Feel free to fork this repository and contribute to make it even better! Submit a pull request with your changes.

---

## License 📜
This project is licensed under the MIT License. See the LICENSE file for details.
