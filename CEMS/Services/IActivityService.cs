using CEMS.Models;

namespace CEMS.Services
{
    public interface IActivityService
    {
        Task<List<Activity>> GetAllActivitiesAsync();
        Task<Activity?> GetActivityByIdAsync(int id);
        Task CreateActivityAsync(Activity activity);
        Task UpdateActivityAsync(Activity activity);
        Task DeleteActivityAsync(int id);
        Task<List<Activity>> FilterActivitiesAsync(string? name, string? category);
    }
}