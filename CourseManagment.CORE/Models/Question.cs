using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        public string QuestionText { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        public string? Options { get; set; }

        public string? CorrectAnswer { get; set; }

        public Quiz Quiz { get; set; } = null!;
    }
}