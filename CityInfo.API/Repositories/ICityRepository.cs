using CityInfo.API.Entities;

namespace CityInfo.API.Repositories
{
    //اگر تعداد جدول ها زیاد بود برا هر جدول یه ریپازیتوری میسازیم
    public interface ICityRepository
    {
        //IEnumerable<City> GetCities();
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<bool> IsCityExistAsync(int cityId);
        Task<City?> GetCityAsync(int cityId, bool includePointOfInterests);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestAsync(int cityId,int pointOfInterestId);
        Task AddPointOfInterestAsync(int cityId,PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}
