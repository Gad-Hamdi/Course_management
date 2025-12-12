using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public int? Duration { get; set; }

        public int? PassingScore { get; set; }

        public bool IsPublished { get; set; }

        public Course Course { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
    }
}