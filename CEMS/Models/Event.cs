using CEMS.Models;
using System.ComponentModel.DataAnnotations;

public class Event
{
    public int EventId { get; set; }

    [Required(ErrorMessage = "Event name is required")]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Please select a venue")]
    public int VenueId { get; set; }
    public Venue? Venue { get; set; }

    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    public ICollection<EventActivity> EventActivities { get; set; } = new List<EventActivity>();
}