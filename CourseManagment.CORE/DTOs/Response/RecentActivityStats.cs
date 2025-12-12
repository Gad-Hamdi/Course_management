using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class RecentActivityStats
    {
        public int EnrollmentsLast30Days { get; set; }
        public int CompletionsLast30Days { get; set; }
        public int QuizAttemptsLast30Days { get; set; }
        public int PointsAwardedLast30Days { get; set; }
    }
}
