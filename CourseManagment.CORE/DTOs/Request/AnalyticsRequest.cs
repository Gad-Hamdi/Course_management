using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Request
{
    public class AnalyticsRequest
    {
        [Required]
        public string Metric { get; set; } = null!; // "enrollments", "completions", "quiz_scores"

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CompanyId { get; set; }
        public int? CourseId { get; set; }
        public string? GroupBy { get; set; } // "day", "week", "month", "course"
    }
}
