using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class QuizDTO
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public int? Duration { get; set; }

        public int? PassingScore { get; set; }

        public bool IsPublished { get; set; }
    }
}
