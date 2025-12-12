using CourseManagment.CORE.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int CompanyId { get; set; }

        public string? JobTitle { get; set; }

        public int Points { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<EmployeeBadge> EmployeeBadges { get; set; } = new List<EmployeeBadge>();
        public virtual ICollection<PointTransaction> PointTransactions { get; set; } = new List<PointTransaction>();

        public string FullName => $"{FirstName} {LastName}";
    }
}