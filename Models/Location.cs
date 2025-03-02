using System.Collections.Generic;

namespace WeatherApp.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string City { get; set; } = string.Empty; // ✅ Fixed: Initialized to empty string
        public string Country { get; set; } = string.Empty; // ✅ Fixed: Initialized
        public List<ApplicationUser> FavoritedBy { get; set; } = new List<ApplicationUser>(); // ✅ Fixed: Initialized list
    }
}
