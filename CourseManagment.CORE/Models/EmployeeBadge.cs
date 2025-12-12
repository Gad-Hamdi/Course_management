using CourseManagment.CORE.Identity;

namespace CourseManagment.CORE.Models
{
    public class EmployeeBadge
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public int BadgeId { get; set; }

        public DateTime AwardedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;
        public Badge Badge { get; set; } = null!;
    }
}