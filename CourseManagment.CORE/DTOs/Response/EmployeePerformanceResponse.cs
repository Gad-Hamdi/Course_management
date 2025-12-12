using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class EmployeePerformanceResponse
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int CompletedCourses { get; set; }
        public decimal AverageQuizScore { get; set; }
        public int TotalPoints { get; set; }
        public int BadgesCount { get; set; }
        public decimal CompletionRate { get; set; }
        public TimeSpan AverageCompletionTime { get; set; }
        public DateTime? LastActivity { get; set; }
    }
}
