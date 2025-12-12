using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? Category { get; set; }

        public int? Duration { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Company Company { get; set; } = null!;
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}