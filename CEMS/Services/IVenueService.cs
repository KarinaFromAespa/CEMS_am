using CEMS.Models;

namespace CEMS.Services
{
    public interface IVenueService
    {
        Task<List<Venue>> GetAllVenuesAsync();
        Task<Venue?> GetVenueByIdAsync(int id);
        Task CreateVenueAsync(Venue venue);
        Task UpdateVenueAsync(Venue venue);
        Task DeleteVenueAsync(int id);
        Task<List<Venue>> FilterVenuesAsync(string? name, string? location);
    }
}