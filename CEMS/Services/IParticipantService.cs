using CEMS.Models;

namespace CEMS.Services
{
    public interface IParticipantService
    {
        Task<List<Participant>> GetAllParticipantsAsync();
        Task<Participant?> GetParticipantByIdAsync(int id);
        Task CreateParticipantAsync(Participant participant);
        Task UpdateParticipantAsync(Participant participant);
        Task DeleteParticipantAsync(int id);
        Task<List<Participant>> FilterParticipantsAsync(string? name, string? email);
        Task<bool> ResetPasswordAsync(int participantId, string newPassword);
    }
}