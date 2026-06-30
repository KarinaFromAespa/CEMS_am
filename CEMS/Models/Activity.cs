using System.ComponentModel.DataAnnotations;

namespace CEMS.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }

        [Required(ErrorMessage = "Activity name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Activity name must be between 2 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Activity type is required")]
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters")]
        public string Type { get; set; } = string.Empty; 

        public ICollection<EventActivity> EventActivities { get; set; } = new List<EventActivity>();
    }
}