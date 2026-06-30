using CEMS.Data;
using CEMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CEMS.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEventsAsync()
            => await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.EventActivities).ThenInclude(ea => ea.Activity)
                .OrderBy(e => e.Date)
                .ToListAsync();

        public async Task<Event?> GetEventByIdAsync(int id)
            => await _context.Events
                .Include(e => e.Venue)
                .Include(e => e.Registrations).ThenInclude(r => r.Participant)
                .Include(e => e.EventActivities).ThenInclude(ea => ea.Activity)
                .FirstOrDefaultAsync(e => e.EventId == id);

        public async Task CreateEventAsync(Event evt)
        {
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event evt)
        {
            _context.Events.Update(evt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt != null)
            {
                _context.Events.Remove(evt);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Event>> FilterEventsAsync(DateTime? date, int? venueId, int? activityId)
        {
            var query = _context.Events
                .Include(e => e.Venue)
                .Include(e => e.EventActivities).ThenInclude(ea => ea.Activity)
                .AsQueryable();

            if (date.HasValue)
                query = query.Where(e => e.Date.Date == date.Value.Date);
            if (venueId.HasValue)
                query = query.Where(e => e.VenueId == venueId.Value);
            if (activityId.HasValue)
                query = query.Where(e => e.EventActivities.Any(ea => ea.ActivityId == activityId.Value));

            return await query.OrderBy(e => e.Date).ToListAsync();
        }
    }
}