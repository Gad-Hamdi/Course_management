using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class CourseAnalyticsResponse
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public int TotalEnrollments { get; set; }
        public int CompletedEnrollments { get; set; }
        public decimal CompletionRate { get; set; }
        public decimal AverageProgress { get; set; }
        public decimal AverageQuizScore { get; set; }
        public TimeSpan AverageCompletionTime { get; set; }
        public int TotalPointsAwarded { get; set; }
    }
}
