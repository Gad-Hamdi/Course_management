using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class CoursePerformanceReport
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public int TotalEnrollments { get; set; }
        public decimal CompletionRate { get; set; }
        public decimal AverageProgress { get; set; }
        public decimal AverageQuizScore { get; set; }
        public List<TopPerformer> TopPerformers { get; set; } = new();
        public List<StrugglingStudent> StrugglingStudents { get; set; } = new();
    }
}
