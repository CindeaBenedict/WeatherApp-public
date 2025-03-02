namespace WeatherApp.Models
{
    public class FavoriteLocation
    {
        public int Id { get; set; }  // Primary Key

        public int LocationId { get; set; }  // Foreign Key for Location
        public Location Location { get; set; } = new Location();  // Navigation property

        public string LocationName { get; set; } = string.Empty; // ✅ FIXED: Added missing property
        public double Latitude { get; set; }  // ✅ FIXED: Added missing property
        public double Longitude { get; set; }  // ✅ FIXED: Added missing property

        public string ApplicationUserId { get; set; } = string.Empty; // ✅ FIXED: Ensure User ID exists
        public ApplicationUser ApplicationUser { get; set; } = new ApplicationUser(); // Ensure it's not null
    }
}
