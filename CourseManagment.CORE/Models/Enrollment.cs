using CourseManagment.CORE.Identity;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }

        public decimal Progress { get; set; } = 0;

        public string Status { get; set; } = "Active";

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}