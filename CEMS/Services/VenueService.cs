using CEMS.Data;
using CEMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CEMS.Services
{
    public class VenueService : IVenueService
    {
        private readonly AppDbContext _context;

        public VenueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Venue>> GetAllVenuesAsync()
            => await _context.Venues
                .OrderBy(v => v.Name)
                .ToListAsync();

        public async Task<Venue?> GetVenueByIdAsync(int id)
            => await _context.Venues
                .Include(v => v.Events)
                .FirstOrDefaultAsync(v => v.VenueId == id);

        public async Task CreateVenueAsync(Venue venue)
        {
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVenueAsync(Venue venue)
        {
            _context.Venues.Update(venue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVenueAsync(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Venue>> FilterVenuesAsync(string? name, string? location)
        {
            var query = _context.Venues.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(v => v.Address.Contains(location));

            return await query.OrderBy(v => v.Name).ToListAsync();
        }
    }
}