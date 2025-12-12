using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class CompanyEngagementReport
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public decimal EnrollmentRate { get; set; }
        public decimal CompletionRate { get; set; }
        public TimeSpan AverageTimeToComplete { get; set; }
        public RecentActivityStats RecentActivity { get; set; } = new();
    }
}
