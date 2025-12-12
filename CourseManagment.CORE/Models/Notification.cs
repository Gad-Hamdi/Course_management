using CourseManagment.CORE.Identity;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;

        public string? Type { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;
    }
}