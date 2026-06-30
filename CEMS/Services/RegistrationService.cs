using CEMS.Data;
using CEMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CEMS.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _context;

        public RegistrationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Registration>> GetAllRegistrationsAsync()
            => await _context.Registrations
                .Include(r => r.Event)
                .Include(r => r.Participant)
                .OrderBy(r => r.RegistrationDate)
                .ToListAsync();

        public async Task<Registration?> GetRegistrationByIdAsync(int id)
            => await _context.Registrations
                .Include(r => r.Event)
                .Include(r => r.Participant)
                .FirstOrDefaultAsync(r => r.RegistrationId == id);

        public async Task CreateRegistrationAsync(Registration registration)
        {
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRegistrationAsync(Registration registration)
        {
            _context.Registrations.Update(registration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRegistrationAsync(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Registration>> FilterRegistrationsAsync(int? eventId, int? participantId)
        {
            var query = _context.Registrations
                .Include(r => r.Event)
                .Include(r => r.Participant)
                .AsQueryable();

            if (eventId.HasValue)
                query = query.Where(r => r.EventId == eventId.Value);
            if (participantId.HasValue)
                query = query.Where(r => r.ParticipantId == participantId.Value);

            return await query.OrderBy(r => r.RegistrationDate).ToListAsync();
        }
    }
}