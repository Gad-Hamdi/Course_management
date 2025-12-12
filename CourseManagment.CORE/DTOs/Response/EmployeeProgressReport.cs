using CourseManagment.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagment.CORE.DTOs.Response
{
    public class EmployeeProgressReport
    {
        public string EmployeeId { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public int TotalEnrollments { get; set; }
        public int CompletedCourses { get; set; }
        public int InProgressCourses { get; set; }
        public decimal AverageQuizScore { get; set; }
        public int CertificatesCount { get; set; }
        public int BadgesCount { get; set; }
        //public List<EnrollmentProgress> CurrentProgress { get; set; } = new();
        public List<SkillDevelopment> SkillDevelopment { get; set; } = new();
    }
}
