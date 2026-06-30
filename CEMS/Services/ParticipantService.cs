using CEMS.Data;
using CEMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CEMS.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly AppDbContext _context;

        public ParticipantService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Participant>> GetAllParticipantsAsync()
            => await _context.Participants
                .OrderBy(p => p.Name)
                .ToListAsync();

        public async Task<Participant?> GetParticipantByIdAsync(int id)
            => await _context.Participants
                .Include(p => p.Registrations)
                    .ThenInclude(r => r.Event)
                .FirstOrDefaultAsync(p => p.ParticipantId == id);

        public async Task CreateParticipantAsync(Participant participant)
        {
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateParticipantAsync(Participant participant)
        {
            _context.Participants.Update(participant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteParticipantAsync(int id)
        {
            var participant = await _context.Participants.FindAsync(id);
            if (participant != null)
            {
                _context.Participants.Remove(participant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Participant>> FilterParticipantsAsync(string? name, string? email)
        {
            var query = _context.Participants.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(p => p.Email.Contains(email));

            return await query.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<bool> ResetPasswordAsync(int participantId, string newPassword)
        {
            var participant = await _context.Participants.FindAsync(participantId);
            if (participant == null) return false;

            participant.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}