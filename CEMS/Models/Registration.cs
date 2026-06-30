namespace CEMS.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled, Waitlisted
    }
}