namespace CEMS.Services
{
    public class SessionService
    {
        public bool IsAdmin { get; private set; } = false;
        public int? CurrentParticipantId { get; private set; } = null;
        public string CurrentParticipantName { get; private set; } = string.Empty;
        public bool IsLoggedIn => CurrentParticipantId.HasValue;

        private const string AdminPin = "admin123";

        public event Action? OnChange;

        public bool TryLogin(string pin)
        {
            if (pin == AdminPin)
            {
                IsAdmin = true;
                OnChange?.Invoke();
                return true;
            }
            return false;
        }

        public void LoginParticipant(int participantId, string name)
        {
            CurrentParticipantId = participantId;
            CurrentParticipantName = name;
            OnChange?.Invoke();
        }

        public void LogoutParticipant()
        {
            CurrentParticipantId = null;
            CurrentParticipantName = string.Empty;
            OnChange?.Invoke();
        }

        public void Logout()
        {
            IsAdmin = false;
            LogoutParticipant();
        }
    }
}