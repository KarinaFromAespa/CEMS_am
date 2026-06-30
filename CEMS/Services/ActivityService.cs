using CEMS.Data;
using CEMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CEMS.Services
{
    public class ActivityService : IActivityService
    {
        private readonly AppDbContext _context;

        public ActivityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> GetAllActivitiesAsync()
            => await _context.Activities
                .OrderBy(a => a.Name)
                .ToListAsync();

        public async Task<Activity?> GetActivityByIdAsync(int id)
            => await _context.Activities
                .Include(a => a.EventActivities)
                    .ThenInclude(ea => ea.Event)
                .FirstOrDefaultAsync(a => a.ActivityId == id);

        public async Task CreateActivityAsync(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActivityAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteActivityAsync(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Activity>> FilterActivitiesAsync(string? name, string? category)
        {
            var query = _context.Activities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(a => a.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(a => a.Type.Contains(category));

            return await query.OrderBy(a => a.Name).ToListAsync();
        }
    }
}