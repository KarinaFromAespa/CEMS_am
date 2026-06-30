using CEMS.Models;

namespace CEMS.Services
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task CreateEventAsync(Event evt);
        Task UpdateEventAsync(Event evt);
        Task DeleteEventAsync(int id);
        Task<List<Event>> FilterEventsAsync(DateTime? date, int? venueId, int? activityId);
    }
}