using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class SystemUsageResponse
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalCourses { get; set; }
        public int TotalEnrollments { get; set; }
        public int CompletedEnrollments { get; set; }
        public int TotalQuizzes { get; set; }
        public int QuizAttempts { get; set; }
        public int CertificatesGenerated { get; set; }
        public int PointsAwarded { get; set; }
        public Dictionary<string, int> DailyActivity { get; set; } = new();
        public Dictionary<string, int> CoursePopularity { get; set; } = new();
    }
}
