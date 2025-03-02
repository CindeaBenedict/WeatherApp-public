using System;

namespace WeatherApp.Models
{
    public class WeatherAlert
    {
        public int Id { get; set; }
        public string AlertType { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Severity { get; set; }
    }
}