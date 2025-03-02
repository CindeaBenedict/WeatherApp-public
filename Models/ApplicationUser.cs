using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WeatherApp.Models;

namespace WeatherApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<FavoriteLocation> FavoriteLocations { get; set; }
        public virtual UserPreference Preferences { get; set; }

        public ApplicationUser()
        {
            FavoriteLocations = new List<FavoriteLocation>();
            Preferences = new UserPreference();  // ✅ FIX: Ensure Preferences is never null
        }
    }
}
