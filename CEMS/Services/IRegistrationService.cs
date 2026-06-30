using CEMS.Models;

namespace CEMS.Services
{
    public interface IRegistrationService
    {
        Task<List<Registration>> GetAllRegistrationsAsync();
        Task<Registration?> GetRegistrationByIdAsync(int id);
        Task CreateRegistrationAsync(Registration registration);
        Task UpdateRegistrationAsync(Registration registration);
        Task DeleteRegistrationAsync(int id);
        Task<List<Registration>> FilterRegistrationsAsync(int? eventId, int? participantId);
    }
}