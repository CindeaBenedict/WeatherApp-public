using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IFavoriteLocationsService
    {
        Task<List<FavoriteLocation>> GetUserFavorites(string userId);
        Task<FavoriteLocation> AddFavorite(FavoriteLocation location);
        Task<bool> RemoveFavorite(int id, string userId);
        Task<bool> IsFavorite(double latitude, double longitude, string userId);
    }

    public class FavoriteLocationsService : IFavoriteLocationsService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteLocationsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FavoriteLocation>> GetUserFavorites(string userId)
        {
            return await _context.FavoriteLocations
                .Where(f => f.ApplicationUserId == userId)
                .ToListAsync();
        }

        public async Task<FavoriteLocation> AddFavorite(FavoriteLocation location)
        {
            _context.FavoriteLocations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<bool> RemoveFavorite(int id, string userId)
        {
            var favorite = await _context.FavoriteLocations
                .FirstOrDefaultAsync(f => f.Id == id && f.ApplicationUserId == userId);

            if (favorite == null)
                return false;

            _context.FavoriteLocations.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsFavorite(double latitude, double longitude, string userId)
        {
            return await _context.FavoriteLocations
                .AnyAsync(f => f.ApplicationUserId == userId && 
                               f.Latitude == latitude && 
                               f.Longitude == longitude);
        }
    }
}
