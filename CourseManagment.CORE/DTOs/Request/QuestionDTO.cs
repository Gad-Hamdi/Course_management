using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class QuestionDTO
    {
        [Required]
        public int QuizId { get; set; }

        [Required]
        public string QuestionText { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        public string? Options { get; set; }

        public string? CorrectAnswer { get; set; }
    }
}
