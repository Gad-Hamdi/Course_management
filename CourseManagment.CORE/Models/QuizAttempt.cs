using CourseManagment.CORE.Identity;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.CORE.Models
{
    public class QuizAttempt
    {
        public int QuizAttemptId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int QuizId { get; set; }

        public int? Score { get; set; }

        public DateTime AttemptDate { get; set; } = DateTime.UtcNow;

        public bool Passed { get; set; }

        public ApplicationUser User { get; set; } = null!;
        public Quiz Quiz { get; set; } = null!;
    }
}