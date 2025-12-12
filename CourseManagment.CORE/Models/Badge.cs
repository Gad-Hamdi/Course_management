using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Models
{
    public class Badge
    {
        public int BadgeId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ICollection<EmployeeBadge> EmployeeBadges { get; set; } = new List<EmployeeBadge>();
    }
}