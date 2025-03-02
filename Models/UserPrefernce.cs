using System.ComponentModel.DataAnnotations;
using WeatherApp.Models;

namespace WeatherApp.Models
{
    public class UserPreference
    {
        public int Id { get; set; }
        public bool ShowTemperatureInCelsius { get; set; } = true;  // ✅ FIX: Set a default value
        public string PreferredLanguage { get; set; } = "en";  // ✅ FIX: Set a default value

        [Required]
        public string? ApplicationUserId { get; set; }  // ✅ FIX

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
