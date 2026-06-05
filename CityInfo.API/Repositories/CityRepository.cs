using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly Context _context;
        public CityRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointOfInterests)
        {
            if (includePointOfInterests)
            {
                return await _context.Cities.Include(c => c.pointOfInterests).FirstOrDefaultAsync(c => c.Id == cityId);
            }

            return await _context.Cities.FindAsync(cityId);

        }

        public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointOfInterests.FirstOrDefaultAsync(p => p.CityId == cityId && p.Id == pointOfInterestId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsForCityAsync(int cityId)
        {
            return await _context.PointOfInterests.Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task<bool> IsCityExistAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task AddPointOfInterestAsync(int cityId, PointOfInterest pointOfInterest)
        {
            pointOfInterest.CityId = cityId;
            await _context.PointOfInterests.AddAsync(pointOfInterest);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterests.Remove(pointOfInterest);
        }
    }
}
